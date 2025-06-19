namespace StoreDashboard.Blazor.Application.Features.Contacts.Commands.Update;

public class UpdateContactCommandValidator : AbstractValidator<UpdateContactCommand>
{
        public UpdateContactCommandValidator()
        {
           RuleFor(v => v.Id).NotNull();
               RuleFor(v => v.Name).MaximumLength(50).NotEmpty(); 
    RuleFor(v => v.Description).MaximumLength(255); 
    RuleFor(v => v.Email).MaximumLength(255); 
    RuleFor(v => v.PhoneNumber).MaximumLength(255); 
    RuleFor(v => v.Country).MaximumLength(255); 

          
        }
    
}

