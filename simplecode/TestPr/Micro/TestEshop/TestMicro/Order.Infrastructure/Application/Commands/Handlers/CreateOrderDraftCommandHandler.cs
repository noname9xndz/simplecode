using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Order.Infrastructure.Application.Commands.Commands;
using Order.Infrastructure.Domain.Services.Interface;
using Order.Infrastructure.Models;
using Order.Infrastructure.Models.DTO;

namespace Order.Infrastructure.Application.Commands.Handlers
{
    public class CreateOrderDraftCommandHandler: IRequestHandler<CreateOrderDraftCommand, OrderDraftDTO>
    {
        // Using DI to inject infrastructure persistence Repositories
        public CreateOrderDraftCommandHandler()
        {
            
        }

        public Task<OrderDraftDTO> Handle(CreateOrderDraftCommand message, CancellationToken cancellationToken)
        {

            var order = Order.Infrastructure.Domain.AggregatesModel.Entities.Order.NewDraft();
            var orderItems = message.Items.Select(i => i.ToOrderItemDTO());
            foreach (var item in orderItems)
            {
                order.AddOrderItem(item.ProductId, item.ProductName, item.UnitPrice, item.Discount, item.PictureUrl, item.Units);
            }

            return Task.FromResult(OrderDraftDTO.FromOrder(order));
        }
    }
}
