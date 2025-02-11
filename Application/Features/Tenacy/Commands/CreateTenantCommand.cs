using Application.Features.Tenacy.Models;
using Application.Models.Wrapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Tenacy.Commands
{
    public class CreateTenantCommand : IRequest<IResponseWrapper>
    {
        public CreateTenantRequest CreateTenant { get; set; }
    }

    public class CreateTenantCommandHandler(ITenantService tenantService) : IRequestHandler<CreateTenantCommand, IResponseWrapper>
    {
        private readonly ITenantService _tenantService = tenantService;
        public async Task<IResponseWrapper> Handle(CreateTenantCommand request, CancellationToken cancellationToken)
        {
            var tenantId = await _tenantService.CreateTenantAsync(request.CreateTenant, cancellationToken);

            return await ResponseWrapper<string>.SuccessAsync(data: tenantId, "Tenant created successfully.");
        }
    }
}
