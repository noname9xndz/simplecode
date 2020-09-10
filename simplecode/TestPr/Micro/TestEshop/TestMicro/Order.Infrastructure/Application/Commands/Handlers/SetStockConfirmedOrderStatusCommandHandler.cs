using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Order.Infrastructure.Application.Commands.Commands;
using Order.Infrastructure.Domain.Services.Interface;

namespace Order.Infrastructure.Application.Commands.Handlers
{
    public class SetStockConfirmedOrderStatusCommandHandler : IRequestHandler<SetStockConfirmedOrderStatusCommand, bool>
    {
        private readonly IOrderService _orderService;

        public SetStockConfirmedOrderStatusCommandHandler(IOrderService orderService)
        {
            _orderService = orderService;
        }

        /// <summary>
        /// Handler which processes the command when
        /// Stock service confirms the request
        /// </summary>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<bool> Handle(SetStockConfirmedOrderStatusCommand command, CancellationToken cancellationToken)
        {
            // Simulate a work time for confirming the stock
            await Task.Delay(10000, cancellationToken);

            var orderToUpdate = await _orderService.GetAsync(command.OrderNumber);
            if (orderToUpdate == null)
            {
                return false;
            }

            orderToUpdate.SetStockConfirmedStatus();
            return await _orderService.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
