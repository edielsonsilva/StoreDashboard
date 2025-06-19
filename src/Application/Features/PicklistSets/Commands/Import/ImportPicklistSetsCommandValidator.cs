namespace StoreDashboard.Blazor.Application.Features.PicklistSets.Commands.Import;

public class ImportPicklistSetsCommandValidator : AbstractValidator<ImportPicklistSetsCommand>
{
    public ImportPicklistSetsCommandValidator()
    {
            RuleFor(x => x.Data).NotNull().NotEmpty();
        }
}