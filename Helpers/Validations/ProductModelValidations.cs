using DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Helpers.Validations
{
    public class ProductModelValidations:AbstractValidator<ProductModel>
    {
        public ProductModelValidations()
        {
            RuleFor(a => a.Name).NotEmpty().WithMessage("Product name is required");
            RuleFor(a => a.Price).NotEmpty().WithMessage("Price name is required");
            RuleFor(a => a.Price).GreaterThan(0).WithMessage("Product price must be greater than zero");
            RuleFor(a => a.Quantity).NotEmpty().WithMessage("Product Quantity is required");
        }
    }
}
