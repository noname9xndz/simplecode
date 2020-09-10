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
    public class SetAwaitingValidationOrderStatusCommandHandler : IRequestHandler<SetAwaitingValidationOrderStatusCommand, bool>
    {
        private readonly IOrderService _orderService;

        public SetAwaitingValidationOrderStatusCommandHandler(IOrderService orderService)
        {
            _orderService = orderService;
        }

        /// <summary>
        /// Handler which processes the command when
        /// graceperiod has finished
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public async Task<bool> Handle(SetAwaitingValidationOrderStatusCommand command, CancellationToken cancellationToken)
        {
            var orderToUpdate = await _orderService.GetAsync(command.OrderNumber);
            if (orderToUpdate == null)
            {
                return false;
            }

            orderToUpdate.SetAwaitingValidationStatus();
            return await _orderService.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
