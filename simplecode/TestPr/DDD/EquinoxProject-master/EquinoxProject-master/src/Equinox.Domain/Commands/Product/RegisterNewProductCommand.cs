using System;
using Equinox.Domain.Commands.Product.Validations;

namespace Equinox.Domain.Commands.Product
{
    public class RegisterNewProductCommand : ProductCommand
    {
        public RegisterNewProductCommand(string name,decimal price)
        {
            Name = name;
            Price = price;
        }

        public override bool IsValid()
        {
            ValidationResult = new RegisterNewProductCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}