using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using GrpcService.Services;

namespace GrpcService.Validator
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(request => request.Name).NotEmpty().WithMessage("Name is not null.");
            RuleFor(request => request.Amount).NotEmpty().WithMessage("Amount is not null.");
        }
    }
}
