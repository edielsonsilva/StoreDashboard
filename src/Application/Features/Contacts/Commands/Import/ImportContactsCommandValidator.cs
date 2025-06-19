namespace StoreDashboard.Blazor.Application.Features.Contacts.Commands.Import;

public class ImportContactsCommandValidator : AbstractValidator<ImportContactsCommand>
{
        public ImportContactsCommandValidator()
        {
           
           RuleFor(v => v.Data)
                .NotNull()
                .NotEmpty();

        }
}

