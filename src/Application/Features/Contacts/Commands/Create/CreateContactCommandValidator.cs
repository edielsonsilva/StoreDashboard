namespace StoreDashboard.Blazor.Application.Features.Contacts.Commands.Create;

public class CreateContactCommandValidator : AbstractValidator<CreateContactCommand>
{
        public CreateContactCommandValidator()
        {
                RuleFor(v => v.Name).MaximumLength(50).NotEmpty(); 
    RuleFor(v => v.Description).MaximumLength(255); 
    RuleFor(v => v.Email).MaximumLength(255); 
    RuleFor(v => v.PhoneNumber).MaximumLength(255); 
    RuleFor(v => v.Country).MaximumLength(255); 

        }
       
}

