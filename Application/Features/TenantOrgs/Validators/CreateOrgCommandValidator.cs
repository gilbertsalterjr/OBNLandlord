using Application.Features.Orgs.Commands;
using FluentValidation;

namespace Application.Features.TenantOrgs.Validators
{
    public class CreateOrgCommandValidator : AbstractValidator<CreateOrgCommand>
    {
        public CreateOrgCommandValidator() 
        {
            RuleFor(command => command.OrgRequest)
                .SetValidator(new CreateOrgRequestValidator());
        }
    }
}
