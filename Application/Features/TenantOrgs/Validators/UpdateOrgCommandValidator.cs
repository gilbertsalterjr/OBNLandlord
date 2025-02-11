using Application.Features.TenantOrgs.Commands;
using FluentValidation;

namespace Application.Features.TenantOrgs.Validators
{
    public class UpdateOrgCommandValidator : AbstractValidator<UpdateOrgCommand>
    {
        public UpdateOrgCommandValidator(IOrgService orgService) 
        {
            RuleFor(command => command.OrgRequest)
                .SetValidator(new UpdateOrgRequestValidator(orgService));
        }
    }
}
