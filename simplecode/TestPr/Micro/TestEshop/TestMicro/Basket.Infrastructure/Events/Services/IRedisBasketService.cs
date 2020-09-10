using Basket.Infrastructure.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Basket.Infrastructure.Events.Services
{
    public interface IRedisBasketService
    {
        Task<CustomerBasket> GetBasketAsync(string customerId);

        IEnumerable<string> GetUsers();

        Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket);

        Task<bool> DeleteBasketAsync(string id);
    }
}