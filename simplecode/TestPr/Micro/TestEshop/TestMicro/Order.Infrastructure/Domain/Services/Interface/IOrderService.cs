
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Order.Infrastructure.Domain.Services.Base;
using Order.Infrastructure.Models.Result;
using CardType = Order.Infrastructure.Domain.AggregatesModel.Entities.CardType;

namespace Order.Infrastructure.Domain.Services.Interface
{
    
    public interface IOrderService : IRepository<AggregatesModel.Entities.Order>
    {
        AggregatesModel.Entities.Order Add(AggregatesModel.Entities.Order order);

        void Update(AggregatesModel.Entities.Order order);

        Task<AggregatesModel.Entities.Order> GetAsync(int orderId);


        Task<Models.Result.Order> GetOrderAsync(int id);

        Task<IEnumerable<OrderSummary>> GetOrdersFromUserAsync(Guid userId);

        Task<IEnumerable<CardType>> GetCardTypesAsync();
    }
}
