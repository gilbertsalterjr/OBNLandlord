using Application.Models.Wrapper;
using MediatR;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Identity.Tokens.Queries
{
    public class GetRefreshTokenQuery : IRequest<IResponseWrapper>
    {
        public RefreshTokenRequest RefreshToken { get; set; }
    }

    public class GetRefreshTokenQueryHandler(ITokenService tokenService) : IRequestHandler<GetRefreshTokenQuery, IResponseWrapper>
    {
        private readonly ITokenService _tokenService = tokenService;

        public async Task<IResponseWrapper> Handle(GetRefreshTokenQuery request, CancellationToken cancellationToken)
        {
            var refreshToken = await _tokenService.RefreshTokenAsync(request.RefreshToken);
            return await ResponseWrapper<TokenResponse>.SuccessAsync(refreshToken);
        }
    }
}
