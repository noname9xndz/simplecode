using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using Order.Infrastructure.Models;
using Order.Infrastructure.Models.DTO;

namespace Order.Infrastructure.Application.Commands.Commands
{
    public class CreateOrderDraftCommand : IRequest<OrderDraftDTO>
    {

        public string BuyerId { get; private set; }

        public IEnumerable<BasketItem> Items { get; private set; }

        public CreateOrderDraftCommand(string buyerId, IEnumerable<BasketItem> items)
        {
            BuyerId = buyerId;
            Items = items;
        }
    }
}
