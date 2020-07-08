using ProductElasticSearchAdvanced.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProductElasticSearchAdvanced.Settings;

namespace ProductElasticSearchAdvanced.Services
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetProductsCache(int count, int skip = 0);

        Task<Product> GetProductByIdCache(int id);

        Task<IEnumerable<Product>> GetProductsByCategoryCache(string category);

        Task<IEnumerable<Product>> GetProductsByBrandCache(string category);

        Task<IEnumerable<Product>> GetAllProducts(int count, int skip = 0);

        Task<Product> GetProductById(int id);

        Task DeleteAsync(Product product);

        Task<int> SaveSingleAsync(Product product);

        Task<int> SaveManyAsync(Product[] products);

        Task<int> SaveBulkAsync(Product[] products);

        Task<IReadOnlyCollection<Product>> FindProducts(string keyword, int page = 1, int pageSize = 5);

        Task<Dictionary<string, object>> GetAggregationsProduct(string query);

        Task<int> ReIndexProduct();

        Task<int> GenerateProducts(int count);

        Task<string> GetProductIndex();
        Task<ProductSettings> GetProductSetting();
    }
}