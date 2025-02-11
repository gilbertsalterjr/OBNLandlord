using Application.Features.Tenacy.Models;
using Application.Models.Wrapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Tenacy.Queries
{
    public class GetTenantsQuery : IRequest<IResponseWrapper>
    {
    }

    public class GetTenantsQueryHandler(ITenantService tenantService) : IRequestHandler<GetTenantsQuery, IResponseWrapper>
    {
        private readonly ITenantService _tenantService = tenantService;
        public async Task<IResponseWrapper> Handle(GetTenantsQuery request, CancellationToken cancellationToken)
        {
            var tenantsInDb = await _tenantService.GetTenantsAsync();
            return await ResponseWrapper<List<TenantDto>>.SuccessAsync(data: tenantsInDb, message: "Tenants retrieved successfully.");
        }
    }
}
