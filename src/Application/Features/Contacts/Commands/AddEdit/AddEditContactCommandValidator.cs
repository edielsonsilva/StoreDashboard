using StoreDashboard.Blazor.Application.Features.Contacts.Commands.AddEdit;

namespace StoreDashboard.Blazor.Application.Features.Contacts.Commands.AddEdit;

public class AddEditContactCommandValidator : AbstractValidator<AddEditContactCommand>
{
    public AddEditContactCommandValidator()
    {
                RuleFor(v => v.Name).MaximumLength(50).NotEmpty(); 
    RuleFor(v => v.Description).MaximumLength(255); 
    RuleFor(v => v.Email).MaximumLength(255); 
    RuleFor(v => v.PhoneNumber).MaximumLength(255); 
    RuleFor(v => v.Country).MaximumLength(255); 

     }

}

