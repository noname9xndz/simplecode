using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Equinox.Domain.Core.Core.Data;
using Equinox.Domain.Models;

namespace Equinox.Domain.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<Product> GetById(Guid id);
        Task<IEnumerable<Product>> GetAll();

        void Add(Product product);
        void Update(Product product);
        void Remove(Product product);
    }
}