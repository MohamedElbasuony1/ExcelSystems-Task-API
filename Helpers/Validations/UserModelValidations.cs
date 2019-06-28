using DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Helpers.Validations
{
    public class UserModelValidations:AbstractValidator<UserModel>
    {
        public UserModelValidations()
        {
            RuleFor(a => a.UserName).NotEmpty().WithMessage("User name is required");
            RuleFor(a => a.Password).NotEmpty().WithMessage("Password is required");
        }
    }
}
