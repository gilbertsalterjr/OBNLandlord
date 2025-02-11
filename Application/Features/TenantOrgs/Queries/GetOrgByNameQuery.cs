using Application.Models.Wrapper;
using MediatR;
using Mapster;

namespace Application.Features.TenantOrgs.Queries
{
    public class GetOrgByNameQuery : IRequest<IResponseWrapper>
    {
        public string Name { get; set; }
    }

    public class GetOrgByNameQueryHandler(IOrgService orgService) : IRequestHandler<GetOrgByNameQuery, IResponseWrapper>
    {
        private readonly IOrgService _orgService = orgService;

        public async Task<IResponseWrapper> Handle(GetOrgByNameQuery request, CancellationToken cancellationToken)
        {
            var orgInDb = (await _orgService.GetOrgByNameAsync(request.Name)).Adapt<OrgResponse>();

            if (orgInDb is not null)
            {
                return await ResponseWrapper<OrgResponse>.SuccessAsync(data: orgInDb);
            }
            return await ResponseWrapper<OrgResponse>.FailAsync("Organization does not exist.");
        }
    }
}
