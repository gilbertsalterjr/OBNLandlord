using Application.Models.Wrapper;
using Domain.Entities;
using Mapster;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.TenantOrgs.Queries
{
    public class GetOrgByIdQuery : IRequest<IResponseWrapper>
    {
        public int OrgId { get; set; }
    }

    public class GetOrgByIdQueryHandler(IOrgService orgService) : IRequestHandler<GetOrgByIdQuery, IResponseWrapper>
    {
        private readonly IOrgService _orgService = orgService;

        public async Task<IResponseWrapper> Handle(GetOrgByIdQuery request, CancellationToken cancellationToken)
        {
            var orgInDb = (await _orgService.GetOrgByIdAsync(request.OrgId)).Adapt<OrgResponse>();

            if (orgInDb is not null) {
                return await ResponseWrapper<OrgResponse>.SuccessAsync(data: orgInDb);                
            }
            return await ResponseWrapper<OrgResponse>.FailAsync("Organization does not exist.");
        }
    }
}
