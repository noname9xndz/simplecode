
using System.Threading.Tasks;
using Order.Infrastructure.Domain.Services.Base;

namespace Order.Infrastructure.Domain.Services.Interface
{
    
    public interface IOrderService : IRepository<AggregatesModel.Entities.Order>
    {
        AggregatesModel.Entities.Order Add(AggregatesModel.Entities.Order order);

        void Update(AggregatesModel.Entities.Order order);

        Task<AggregatesModel.Entities.Order> GetAsync(int orderId);
    }
}
