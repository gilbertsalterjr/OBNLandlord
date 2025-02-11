using Domain.Entities;
using FluentValidation;

namespace Application.Features.TenantOrgs.Validators
{
    internal class UpdateOrgRequestValidator : AbstractValidator<UpdateOrgRequest>
    {
        public UpdateOrgRequestValidator(IOrgService orgService) 
        {
            RuleFor(request => request.Id)
                .NotEmpty()
                .MustAsync(async (id, ct) => await orgService.GetOrgByIdAsync(id) is TenantOrg orgInDb && orgInDb.Id == id)
                        .WithMessage("Organization does not exist.");

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
