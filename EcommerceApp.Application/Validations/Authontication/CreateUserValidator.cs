using EcommerceApp.Application.DTOs.Identity;
using FluentValidation;

namespace EcommerceApp.Application.Validations.Authontication;
public class CreateUserValidator : AbstractValidator<CreateUser>
{
    public CreateUserValidator()
    {
        RuleFor(x=>x.FullName)
            .NotEmpty()
            .WithMessage("Full name is required.");

        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email is required.")
            .EmailAddress()
            .WithMessage("Invalid email format.");

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("Password is requred.")
            .MinimumLength(8).WithMessage("Password must be at lest 8 chrachter")
            .Matches(@"[A-Z]").WithMessage("Password must contain at least 1 uppercase chracter. ") 
            .Matches(@"[a-z]").WithMessage("Password must contain at least 1 lowercase chracter.")
            .Matches(@"\d").WithMessage("Password must contain at least one number.")
            .Matches(@"[^\w]").WithMessage("Password must contain at least one spachial chracter.");

        RuleFor(x => x.ConfirmPassword)
            //.NotEmpty()
            .Equal(x => x.Password).WithMessage("Password do not match.");
           
    }
}
