using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using MediatR;

namespace Order.Infrastructure.Application.Commands.Commands
{
    public class SetPaidOrderStatusCommand : IRequest<bool>
    {

        [DataMember]
        public int OrderNumber { get; private set; }

        public SetPaidOrderStatusCommand(int orderNumber)
        {
            OrderNumber = orderNumber;
        }
    }
}
