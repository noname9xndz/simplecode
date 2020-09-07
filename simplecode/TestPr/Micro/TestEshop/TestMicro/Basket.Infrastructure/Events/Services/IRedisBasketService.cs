using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Basket.Infrastructure.Models;

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
