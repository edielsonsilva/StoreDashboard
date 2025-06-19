namespace StoreDashboard.Blazor.Application.Features.PicklistSets.Commands.AddEdit;

public class AddEditPicklistSetCommandValidator : AbstractValidator<AddEditPicklistSetCommand>
{
    public AddEditPicklistSetCommandValidator()
    {
            RuleFor(v => v.Name).NotNull();
            RuleFor(v => v.Text).MaximumLength(50).NotEmpty();
            RuleFor(v => v.Value).MaximumLength(100).NotEmpty();
        }
}