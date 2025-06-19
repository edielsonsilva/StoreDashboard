namespace StoreDashboard.Blazor.Application.Features.Documents.Commands.Delete;

public class DeleteDocumentCommandValidator : AbstractValidator<DeleteDocumentCommand>
{
    public DeleteDocumentCommandValidator()
    {
            RuleFor(x => x.Id).NotNull().NotEmpty();
        }
}