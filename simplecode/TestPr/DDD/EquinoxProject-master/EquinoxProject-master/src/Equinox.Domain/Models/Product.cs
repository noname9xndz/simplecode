using System;
using Equinox.Domain.Core.Core.Domain;

namespace Equinox.Domain.Models
{
    
    public class Product : Entity, IAggregateRoot
    {
        public Product(Guid id, string name, decimal price)
        {
            Id = id;
            Name = name;
            Price = price;
        }

        protected Product() { }

        public string Name { get; private set; }

        public decimal Price { get; private set; }

    }
}
