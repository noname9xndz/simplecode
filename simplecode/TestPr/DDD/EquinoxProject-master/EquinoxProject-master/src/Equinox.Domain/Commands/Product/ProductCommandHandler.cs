using System;
using System.Threading;
using System.Threading.Tasks;
using Equinox.Domain.Core.Core.Messaging;
using Equinox.Domain.Events.Customer;
using Equinox.Domain.Events.Product;
using Equinox.Domain.Interfaces;
using FluentValidation.Results;
using MediatR;

namespace Equinox.Domain.Commands.Product
{
    public class ProductCommandHandler : CommandHandler,
        IRequestHandler<RegisterNewProductCommand, ValidationResult>,
        IRequestHandler<UpdateProductCommand, ValidationResult>,
        IRequestHandler<RemoveProductCommand, ValidationResult>
    {
        private readonly IProductRepository _productRepository;

        public ProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ValidationResult> Handle(RegisterNewProductCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid()) return message.ValidationResult;

            var product = new Models.Product(Guid.NewGuid(), message.Name, message.Price);

            product.AddDomainEvent(new ProductRegisteredEvent(product.Id, product.Name, product.Price));

            _productRepository.Add(product);

            return await Commit(_productRepository.UnitOfWork);
        }

        public async Task<ValidationResult> Handle(UpdateProductCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid()) return message.ValidationResult;

            var product = new Models.Product(message.Id, message.Name, message.Price);
            var existingProduct = await _productRepository.GetById(product.Id);

            if (existingProduct != null && existingProduct.Id != product.Id)
            {
                if (!existingProduct.Equals(product))
                {
                    AddError("The product has already been taken.");
                    return ValidationResult;
                }
            }

            product.AddDomainEvent(new ProductUpdatedEvent(product.Id, product.Name, product.Price));

            _productRepository.Update(product);

            return await Commit(_productRepository.UnitOfWork);
        }

        public async Task<ValidationResult> Handle(RemoveProductCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid()) return message.ValidationResult;

            var product = await _productRepository.GetById(message.Id);

            if (product is null)
            {
                AddError("The product doesn't exists.");
                return ValidationResult;
            }

            product.AddDomainEvent(new ProductRemovedEvent(message.Id));

            _productRepository.Remove(product);

            return await Commit(_productRepository.UnitOfWork);
        }

        public void Dispose()
        {
            _productRepository.Dispose();
        }
    }
}