using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Order.Infrastructure.Application.Commands.Commands;
using Order.Infrastructure.Application.Commands.Handlers.Identified;
using Order.Infrastructure.Domain.Services.Interface;

namespace Order.Infrastructure.Application.Commands.Handlers
{
    public class SetStockRejectedOrderStatusCommandHandler : IRequestHandler<SetStockRejectedOrderStatusCommand, bool>
    {
        private readonly IOrderService _orderService;

        public SetStockRejectedOrderStatusCommandHandler(IOrderService orderService)
        {
            _orderService = orderService;
        }

        /// <summary>
        /// Handler which processes the command when
        /// Stock service rejects the request
        /// </summary>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<bool> Handle(SetStockRejectedOrderStatusCommand command, CancellationToken cancellationToken)
        {
            // Simulate a work time for rejecting the stock
            await Task.Delay(10000, cancellationToken);

            var orderToUpdate = await _orderService.GetAsync(command.OrderNumber);
            if (orderToUpdate == null)
            {
                return false;
            }

            orderToUpdate.SetCancelledStatusWhenStockIsRejected(command.OrderStockItems);

            return await _orderService.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
