using Application.Features.TenantOrgs;
using Application.Models.Wrapper;
using Application.Pipelines;
using Domain.Entities;
using Mapster;
using MediatR;

namespace Application.Features.Orgs.Commands
{
    public class CreateOrgCommand : IRequest<IResponseWrapper>, IValidateMe
    {
        public CreateOrgRequest OrgRequest { get; set; }

    }

    public class CreateOrgCommandHandler(IOrgService orgService) : IRequestHandler<CreateOrgCommand, IResponseWrapper>
    {
        private readonly IOrgService _orgService = orgService;

        public async Task<IResponseWrapper> Handle(CreateOrgCommand request, CancellationToken cancellationToken)
        {
            var newOrg = request.OrgRequest.Adapt<TenantOrg>();
            var orgId = await _orgService.CreateOrgAsync(newOrg);
            return await ResponseWrapper<int>.SuccessAsync(data: orgId, "Organization created successfully");
        }
    }
    
    
}
