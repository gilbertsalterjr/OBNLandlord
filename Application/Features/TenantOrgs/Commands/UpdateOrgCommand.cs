using Application.Models.Wrapper;
using Application.Pipelines;
using MediatR;

namespace Application.Features.TenantOrgs.Commands
{
    public class UpdateOrgCommand : IRequest<IResponseWrapper>, IValidateMe
    {
        public UpdateOrgRequest OrgRequest { get; set; }
    }

    public class UpdateOrgCommandHandler(IOrgService orgService) : IRequestHandler<UpdateOrgCommand, IResponseWrapper>
    {
        private readonly IOrgService _orgService = orgService;

        public async Task<IResponseWrapper> Handle(UpdateOrgCommand request, CancellationToken cancellationToken)
        {
            var orgInDb = await _orgService.GetOrgByIdAsync(request.OrgRequest.Id);
            orgInDb.Name = request.OrgRequest.Name;
            orgInDb.EstablishedOn = request.OrgRequest.EstablishedOn;

            var updatedOrgId = await _orgService.UpdateOrgAsync(orgInDb);
            
            return await ResponseWrapper<int>.SuccessAsync(data: updatedOrgId, "Organization updated successfully");
        }
    }
}
