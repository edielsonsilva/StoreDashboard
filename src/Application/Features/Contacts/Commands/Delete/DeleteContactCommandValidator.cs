namespace StoreDashboard.Blazor.Application.Features.Contacts.Commands.Delete;

public class DeleteContactCommandValidator : AbstractValidator<DeleteContactCommand>
{
        public DeleteContactCommandValidator()
        {
          
            RuleFor(v => v.Id).NotNull().ForEach(v=>v.GreaterThan(0));
          
        }
}
    

