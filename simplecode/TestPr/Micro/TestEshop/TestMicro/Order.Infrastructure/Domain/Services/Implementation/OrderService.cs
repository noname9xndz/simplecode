using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Order.Infrastructure.Context;
using Order.Infrastructure.Domain.Services.Base;
using Order.Infrastructure.Domain.Services.Interface;

namespace Order.Infrastructure.Domain.Services.Implementation
{
    public class OrderService: IOrderService
    {
        private readonly OrderDbContext _context;

        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _context;
            }
        }

        public OrderService(OrderDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public AggregatesModel.Entities.Order Add(AggregatesModel.Entities.Order order)
        {
            return _context.Orders.Add(order).Entity;

        }

        public async Task<AggregatesModel.Entities.Order> GetAsync(int orderId)
        {
            var order = await _context
                .Orders
                .Include(x => x.Address)
                .FirstOrDefaultAsync(o => o.Id == orderId);
            if (order == null)
            {
                order = _context
                    .Orders
                    .Local
                    .FirstOrDefault(o => o.Id == orderId);
            }
            if (order != null)
            {
                await _context.Entry(order)
                    .Collection(i => i.OrderItems).LoadAsync();
                await _context.Entry(order)
                    .Reference(i => i.OrderStatus).LoadAsync();
            }

            return order;
        }

        public void Update(AggregatesModel.Entities.Order order)
        {
            _context.Entry(order).State = EntityState.Modified;
        }
    }
}
