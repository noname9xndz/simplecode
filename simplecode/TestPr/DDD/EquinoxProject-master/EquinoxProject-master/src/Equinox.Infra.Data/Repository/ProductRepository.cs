using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Equinox.Domain.Core.Core.Data;
using Equinox.Domain.Interfaces;
using Equinox.Domain.Models;
using Equinox.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Equinox.Infra.Data.Repository
{
    public class ProductRepository : IProductRepository
    {
        protected readonly EquinoxContext Db;
        protected readonly DbSet<Product> DbSet;

        public ProductRepository(EquinoxContext context)
        {
            Db = context;
            DbSet = Db.Set<Product>();
        }

        public IUnitOfWork UnitOfWork => Db;

        public async Task<Product> GetById(Guid id)
        {
            return await DbSet.FindAsync(id);
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            return await DbSet.ToListAsync();
        }

        public void Add(Product product)
        {
           DbSet.Add(product);
        }

        public void Update(Product product)
        {
            DbSet.Update(product);
        }

        public void Remove(Product product)
        {
            DbSet.Remove(product);
        }

        public void Dispose()
        {
            Db.Dispose();
        }
    }
}
