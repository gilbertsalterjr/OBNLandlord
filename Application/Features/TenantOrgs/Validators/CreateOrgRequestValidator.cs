using Application.Features.Orgs;
using FluentValidation;

namespace Application.Features.TenantOrgs.Validators
{
    internal class CreateOrgRequestValidator : AbstractValidator<CreateOrgRequest>
    {
        public CreateOrgRequestValidator() 
        {
            RuleFor(request => request.Name)
                .NotEmpty()
                    .WithMessage("Org name is required")
                .MaximumLength(200)
                    .WithMessage("Org name should not exceed 200 character length");
            RuleFor(request => request.EstablishedOn)
                .LessThanOrEqualTo(DateTime.UtcNow)
                    .WithMessage("Established date should not be greater than current date");
        }
    }
}
