using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace GrpcService.Services
{
    public class GrpcCrudService : ProductService.ProductServiceBase
    {
        private readonly List<Product> _products = new List<Product>();
        private int idCount = 0;

        private readonly ILogger<GrpcCrudService> _logger;

        public GrpcCrudService(ILogger<GrpcCrudService> logger)
        {
            _logger = logger;
            _products.Add(new Product()
            {
                ProductId = idCount++,
                Name = "Farm Flour",
                Amount = 10,
                Brand = "Bill's Corn",
                Value = 2.33f
            });
        }

        public override Task<ProductList> GetAll(Empty empty, ServerCallContext context)
        {
            ProductList pl = new ProductList();
            pl.Products.AddRange(_products);

            return Task.FromResult(pl);
        }

        public override Task<Product> Get(ProductId productId, ServerCallContext context)
        {
            return Task.FromResult((from p in _products where p.ProductId == productId.Id select p).FirstOrDefault());
        }

        public override Task<Product> Insert(Product product, ServerCallContext context)
        {
            product.ProductId = idCount++;
            _products.Add(product);

            return Task.FromResult(product);
        }

        public override Task<Product> Update(Product product, ServerCallContext context)
        {
            var productToUpdate = (from p in _products where p.ProductId == product.ProductId select p).FirstOrDefault();
            if (productToUpdate != null)
            {
                productToUpdate.Name = product.Name;
                productToUpdate.Amount = product.Amount;
                productToUpdate.Brand = product.Brand;
                productToUpdate.Value = product.Value;

                return Task.FromResult(product);
            }

            return Task.FromException<Product>(new EntryPointNotFoundException());
        }

        public override Task<Empty> Delete(ProductId productId, ServerCallContext context)
        {
            var productToDelete = (from p in _products where p.ProductId == productId.Id select p).FirstOrDefault();
            if (productToDelete == null)
            {
                return Task.FromException<Empty>(new EntryPointNotFoundException());
            }

            _products.Remove(productToDelete);

            return Task.FromResult(new Empty());
        }
    }
}