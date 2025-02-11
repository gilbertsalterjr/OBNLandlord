using Application.Models.Wrapper;
using Application.Pipelines;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.TenantOrgs.Commands
{
    public class DeleteOrgCommand : IRequest<IResponseWrapper>, IValidateMe
    {
        public int OrgId { get; set; }
    }

    public class DeleteOrgCommandHandler(IOrgService orgService) : IRequestHandler<DeleteOrgCommand, IResponseWrapper>
    {
        private readonly IOrgService _orgService = orgService;

        public async Task<IResponseWrapper> Handle(DeleteOrgCommand request, CancellationToken cancellationToken)
        {
            var orgInDb = await _orgService.GetOrgByIdAsync(request.OrgId);
            var deletedOrgId = await _orgService.DeleteOrgAsync(orgInDb);

            return await ResponseWrapper<int>.SuccessAsync(data: deletedOrgId, "Organization deleted successfully");
        }
    }
}
