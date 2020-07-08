using Bogus;
using Elasticsearch.Net;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Nest;
using ProductElasticSearchAdvanced.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using ProductElasticSearchAdvanced.Settings;

namespace ProductElasticSearchAdvanced.Services
{
    public class ProductService : IProductService
    {
        private List<Product> _cache = new List<Product>();

        private readonly IElasticClient _elasticClient;
        private readonly ILogger _logger;
        public IConfiguration Configuration;
        public IOptionsMonitor<ProductSettings> _settings;

        public ProductService(IElasticClient elasticClient, ILogger<ProductService> logger,
            IConfiguration configuration , IOptionsMonitor<ProductSettings> settings)
        {
            _elasticClient = elasticClient;
            _logger = logger;
            Configuration = configuration;
            _settings = settings;
        }

        public virtual Task<IEnumerable<Product>> GetProductsCache(int count, int skip = 0)
        {
            var products = _cache
                .Where(p => p.ReleaseDate <= DateTime.UtcNow)
                .Skip(skip)
                .Take(count);

            return Task.FromResult(products);
        }

        public virtual Task<Product> GetProductByIdCache(int id)
        {
            var product = _cache
              .Where(p => p.ReleaseDate <= DateTime.UtcNow)
              .FirstOrDefault(p => p.Id == id);

            return Task.FromResult(product);
        }

        public virtual Task<IEnumerable<Product>> GetProductsByBrandCache(string brand)
        {
            var products = _cache
                .Where(p => p.ReleaseDate <= DateTime.UtcNow)
                .Where(p => p.Brand.Name.Contains(brand, StringComparison.CurrentCultureIgnoreCase));

            return Task.FromResult(products);
        }

        public virtual Task<IEnumerable<Product>> GetProductsByCategoryCache(string category)
        {
            var products = _cache
                .Where(p => p.ReleaseDate <= DateTime.UtcNow)
                .Where(p => p.Category.Name.Contains(category, StringComparison.CurrentCultureIgnoreCase));

            return Task.FromResult(products);
        }

        public async Task<IEnumerable<Product>> GetAllProducts(int page = 1, int pageSize = 5)
        {
            var response = await _elasticClient.SearchAsync<Product>(
                s => s
                    .From((page - 1) * pageSize)
                    .Size(pageSize));
            return response.Documents;
        }

        public async Task<Product> GetProductById(int id)
        {
            var index = await GetProductIndex();
            var response = await _elasticClient.GetAsync<Product>(id, i => i.Index(index));
            return response.Source;
        }

        public async Task DeleteAsync(Product product)
        {
            await _elasticClient.DeleteAsync<Product>(product);

            if (_cache.Contains(product))
            {
                _cache.Remove(product);
            }
        }

        public async Task<int> SaveSingleAsync(Product product)
        {
            try
            {
                if (_cache.Any(p => p.Id == product.Id))
                {
                    await _elasticClient.UpdateAsync<Product>(product, u => u.Doc(product));
                }
                else
                {
                    _cache.Add(product);
                    await _elasticClient.IndexDocumentAsync<Product>(product);
                }
            }
            catch (ElasticsearchClientException ex)
            {
                return 0;
            }

            return 1;
        }

        public async Task<int> SaveManyAsync(Product[] products)
        {
            _cache.AddRange(products);
            var result = await _elasticClient.IndexManyAsync(products);
            if (result.Errors)
            {
                // the response can be inspected for errors
                foreach (var itemWithError in result.ItemsWithErrors)
                {
                    _logger.LogError("Failed to index document {0}: {1}",
                        itemWithError.Id, itemWithError.Error);
                }
                return 0;
            }
            return 1;
        }

        public async Task<int> SaveBulkAsync(Product[] products)
        {
            _cache.AddRange(products);
            var index = await GetProductIndex();
            var result = await _elasticClient.BulkAsync(b => b.Index(index).IndexMany(products));
            if (result.Errors)
            {
                var count = result.ItemsWithErrors.Count();
                // the response can be inspected for errors
                foreach (var itemWithError in result.ItemsWithErrors)
                {
                    _logger.LogError("Failed to index document {0}: {1}",
                        itemWithError.Id, itemWithError.Error);
                }

                return 0;
            }

            return 1;
        }

        public async Task<IReadOnlyCollection<Product>> FindProducts(string keyword, int page = 1, int pageSize = 5)
        {
            var response = await _elasticClient.SearchAsync<Product>(
                s => s.Query(q => q.QueryString(d => d.Query('*' + keyword + '*')))
                    .From((page - 1) * pageSize)
                    .Size(pageSize));

            if (!response.IsValid)
            {
                // handle errors here by checking response.OriginalException
                //or response.ServerError properties
                _logger.LogError("Failed to search documents");
                return new Product[] { };
            }

            return response.Documents;
        }

        public async Task<Dictionary<string, object>> GetAggregationsProduct(string query)
        {
            var response = await _elasticClient.SearchAsync<Product>(
                  s => s.Query(q => q.QueryString(d => d.Query('*' + query + '*')))
                      .Aggregations(aggs => aggs
                         .Average("average_price", g => g.Field(p => p.Price))
                         .Max("max_price", g => g.Field(p => p.Price))
                         .Min("min_price", g => g.Field(p => p.Price))
                         .Terms("products_for_category", g => g.Field(p => p.Category.Name.Suffix("keyword")))
                         .Terms("products_for_brand", g => g.Field(p => p.Brand.Name.Suffix("keyword")))
                         .Terms("products_for_store", g => g.Field(p => p.Store.Name.Suffix("keyword")))
                     ).RequestConfiguration(r => r
                         .DisableDirectStreaming()
                     )
                  );

            if (!response.IsValid)
            {
                _logger.LogError("Failed to search documents");
            }

            var aggregations = new Dictionary<string, object>();

            var pricesAggregates = new string[] { "average_price", "max_price", "min_price" };
            var productsAggregates = new string[] { "products_for_category", "products_for_brand", "products_for_store" };

            foreach (var aggregation in response.Aggregations)
            {
                if (aggregation.Value is ValueAggregate)
                {
                    var value = (aggregation.Value as ValueAggregate).Value;
                    aggregations.Add(aggregation.Key, value);
                }
                else if (aggregation.Value is BucketAggregate)
                {
                    var value = response.Aggregations.Terms(aggregation.Key).Buckets.ToDictionary(b => b.Key, b => b.DocCount).ToList();
                    aggregations.Add(aggregation.Key, value);
                }
            }

            return aggregations;
        }

        public async Task<int> ReIndexProduct()
        {
            await _elasticClient.DeleteByQueryAsync<Product>(q => q.MatchAll());

            var allProducts = (await GetAllProducts(int.MaxValue)).ToArray();

            foreach (var product in allProducts)
            {
                await _elasticClient.IndexDocumentAsync(product);
            }

            return allProducts.Length;
        }

        public async Task<int> GenerateProducts(int count)
        {
            var storeFaker = new Faker<Store>()
                 .CustomInstantiator(f => new Store())
                 .RuleFor(p => p.Id, f => f.IndexFaker)
                 .RuleFor(p => p.Name, f => f.Person.Company.Name)
                 .RuleFor(p => p.Description, f => f.Lorem.Sentence(f.Random.Int(5, 20)));
            var stores = storeFaker.Generate(4);

            var brandFaker = new Faker<Brand>()
                   .CustomInstantiator(f => new Brand())
                   .RuleFor(p => p.Id, f => f.IndexFaker)
                   .RuleFor(p => p.Name, f => f.Company.CompanyName())
                   .RuleFor(p => p.Description, f => f.Lorem.Sentence(f.Random.Int(5, 20)));
            var brands = brandFaker.Generate(20);

            var categoryFaker = new Faker<Category>()
                 .CustomInstantiator(f => new Category())
                 .RuleFor(p => p.Id, f => f.IndexFaker)
                 .RuleFor(p => p.Name, f => f.Commerce.Categories(1).First())
                 .RuleFor(p => p.Description, f => f.Lorem.Sentence(f.Random.Int(5, 20)));
            var categories = categoryFaker.Generate(30);

            var userFaker = new Faker<User>()
               .CustomInstantiator(f => new User())
               .RuleFor(p => p.Id, f => f.IndexFaker)
               .RuleFor(p => p.FirstName, f => f.Person.FirstName)
               .RuleFor(p => p.LastName, f => f.Person.LastName)
               .RuleFor(p => p.Username, f => f.Person.UserName)
               .RuleFor(p => p.IPAddress, f => f.Internet.Ip());
            var users = userFaker.Generate(1000);

            var reviewFaker = new Faker<Review>()
              .CustomInstantiator(f => new Review())
              .RuleFor(p => p.Id, f => f.IndexFaker)
              .RuleFor(p => p.Rating, f => f.Random.Float(0, 1))
              .RuleFor(p => p.Description, f => f.Person.LastName)
              .RuleFor(p => p.User, f => f.PickRandom(users))
              .RuleFor(p => p.Date, f => f.Date.Past(2));
            var reviews = reviewFaker.Generate(2000).ToArray();

            var productFaker = new Faker<Product>()
                  .CustomInstantiator(f => new Product())
                  .RuleFor(p => p.Id, f => f.IndexFaker)
                  .RuleFor(p => p.Ean, f => f.Commerce.Ean13())
                  .RuleFor(p => p.Name, f => f.Commerce.ProductName())
                  .RuleFor(p => p.Description, f => f.Lorem.Sentence(f.Random.Int(5, 20)))
                  .RuleFor(p => p.Brand, f => f.PickRandom(brands))
                  .RuleFor(p => p.Category, f => f.PickRandom(categories))
                  .RuleFor(p => p.Store, f => f.PickRandom(stores))
                  .RuleFor(p => p.Price, f => f.Finance.Amount())
                  .RuleFor(p => p.Currency, "€")
                  .RuleFor(p => p.Quantity, f => f.Random.Int(0, 1000))
                  .RuleFor(p => p.Rating, f => f.Random.Float(0, 1))
                  .RuleFor(p => p.ReleaseDate, f => f.Date.Past(2))
                  .RuleFor(p => p.Image, f => f.Image.PicsumUrl())
                  .RuleFor(p => p.Reviews, f => f.Random.ArrayElements(reviews, f.Random.Int(0, 50)).ToList())
                  ;

            var products = productFaker.Generate(count);

            return await SaveBulkAsync(products.ToArray());
        }

        public async Task<string> GetProductIndex()
        {
            return await Task.FromResult(Configuration["elasticsearch:index"]);
        }

        public async Task<ProductSettings> GetProductSetting()
        {
            var setting = _settings.CurrentValue;
            return await Task.FromResult(setting);
        }
    }
}