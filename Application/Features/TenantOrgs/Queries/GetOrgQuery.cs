using Application.Models.Wrapper;
using MediatR;
using Mapster;

namespace Application.Features.TenantOrgs.Queries
{
    public class GetOrgsQuery : IRequest<IResponseWrapper>
    {
        public List<OrgResponse> tenantOrgs { get; set; }
    }

    public class GetOrgQueryHandler(IOrgService orgService) : IRequestHandler<GetOrgsQuery, IResponseWrapper>
    {
        private readonly IOrgService _orgService = orgService;

        public async Task<IResponseWrapper> Handle(GetOrgsQuery request, CancellationToken cancellationToken)
        {
            var orgsInDb = await _orgService.GetAllOrgsAsync();

            if (orgsInDb.Count > 0 )
            {
                return await ResponseWrapper<List<OrgResponse>>.SuccessAsync(data: orgsInDb.Adapt<List<OrgResponse>>());
            }
            return await ResponseWrapper<OrgResponse>.FailAsync("No Organizations were found.");
            
        }
    }
}
