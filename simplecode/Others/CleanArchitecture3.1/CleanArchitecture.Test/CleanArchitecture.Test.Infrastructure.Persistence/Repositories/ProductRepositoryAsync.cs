using CleanArchitecture.Test.Application.Interfaces.Repositories;
using CleanArchitecture.Test.Domain.Entities;
using CleanArchitecture.Test.Infrastructure.Persistence.Contexts;
using CleanArchitecture.Test.Infrastructure.Persistence.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Test.Infrastructure.Persistence.Repositories
{
    public class ProductRepositoryAsync : GenericRepositoryAsync<Product>, IProductRepositoryAsync
    {
        private readonly DbSet<Product> _products;

        public ProductRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _products = dbContext.Set<Product>();
        }

        public Task<bool> IsUniqueBarcodeAsync(string barcode)
        {
            return _products
                .AllAsync(p => p.Barcode != barcode);
        }
    }
}
