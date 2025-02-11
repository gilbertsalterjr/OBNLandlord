using Application.Features.TenantOrgs.Commands;
using Domain.Entities;
using FluentValidation;

namespace Application.Features.TenantOrgs.Validators
{
    public class DeleteOrgCommandValidator : AbstractValidator<DeleteOrgCommand>
    {
        public DeleteOrgCommandValidator(IOrgService orgService)
        {
            RuleFor(command => command.OrgId)
                .NotEmpty()
                .MustAsync(async (id, ct) => await orgService.GetOrgByIdAsync(id) is TenantOrg orgInDb && orgInDb.Id == id)
                        .WithMessage("Organization does not exist.");
        }
    }
}
