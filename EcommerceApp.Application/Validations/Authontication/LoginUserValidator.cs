﻿using EcommerceApp.Application.DTOs.Identity;
using FluentValidation;

namespace EcommerceApp.Application.Validations.Authontication;

public class LoginUserValidator : AbstractValidator<LoginUser>
{
    public LoginUserValidator()
    {
        RuleFor(x => x.Email)
           .NotEmpty()
           .WithMessage("Email is required.")
           .EmailAddress()
           .WithMessage("Invalid email format.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is requred.");
    }
}
