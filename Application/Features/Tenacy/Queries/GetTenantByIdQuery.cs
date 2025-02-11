using Application.Features.Tenacy.Models;
using Application.Models.Wrapper;
using MediatR;
using System.Net.Http.Headers;

namespace Application.Features.Tenacy.Queries
{
    public class GetTenantByIdQuery : IRequest<IResponseWrapper>
    {
        public string TenantId { get; set; }
    }

    public class GetTenantByIdQueryHandler(ITenantService tenantService) : IRequestHandler<GetTenantByIdQuery, IResponseWrapper>
    {
        private readonly ITenantService _tenantService = tenantService;
        
        public async Task<IResponseWrapper> Handle(GetTenantByIdQuery request, CancellationToken cancellationToken)
        {
            var tenantInDb = await _tenantService.GetTenantByIdAsync(request.TenantId);
            if ( tenantInDb is not null)
            {
                return await ResponseWrapper<TenantDto>.SuccessAsync(data: tenantInDb);
            }
            return await ResponseWrapper<TenantDto>.FailAsync(message: "Tenant does not exist.");
        }
    }
}
