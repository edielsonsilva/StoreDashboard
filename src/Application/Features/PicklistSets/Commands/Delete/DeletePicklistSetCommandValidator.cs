namespace StoreDashboard.Blazor.Application.Features.PicklistSets.Commands.Delete;

public class DeletePicklistSetCommandValidator : AbstractValidator<DeletePicklistSetCommand>
{
    public DeletePicklistSetCommandValidator()
    {
            RuleFor(x => x.Id).NotNull().NotEmpty();
        }
}