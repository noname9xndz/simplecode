using System.Collections.Generic;
using System.Runtime.Serialization;
using MediatR;

namespace Order.Infrastructure.Application.Commands.Commands
{
    public class SetStockRejectedOrderStatusCommand : IRequest<bool>
    {

        [DataMember]
        public int OrderNumber { get; private set; }

        [DataMember]
        public List<int> OrderStockItems { get; private set; }

        public SetStockRejectedOrderStatusCommand(int orderNumber, List<int> orderStockItems)
        {
            OrderNumber = orderNumber;
            OrderStockItems = orderStockItems;
        }
    }
}
