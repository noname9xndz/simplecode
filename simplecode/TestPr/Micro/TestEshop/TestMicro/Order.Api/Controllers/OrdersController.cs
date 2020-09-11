using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Event.Bus.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Order.Infrastructure.Application.Commands.Commands;
using Order.Infrastructure.Application.Commands.Commands.Identified;
using Order.Infrastructure.Domain.AggregatesModel.Entities;
using Order.Infrastructure.Domain.Services.Interface;
using Order.Infrastructure.Models.DTO;
using Order.Infrastructure.Models.Result;
using CardType = Order.Infrastructure.Models.Result.CardType;

namespace Order.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [Authorize]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IOrderService _orderService;
        private readonly IIdentityService _identityService;
        private readonly ILogger<OrdersController> _logger;

        public OrdersController(
            IMediator mediator,
            IOrderService orderService,
            IIdentityService identityService,
            ILogger<OrdersController> logger)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _orderService = orderService ?? throw new ArgumentNullException(nameof(orderService));
            _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [Route("cancel")]
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CancelOrderAsync([FromBody] CancelOrderCommand command, [FromHeader(Name = "x-requestid")] string requestId)
        {
            bool commandResult = false;

            if (Guid.TryParse(requestId, out Guid guid) && guid != Guid.Empty)
            {
                var requestCancelOrder = new IdentifiedCommand<CancelOrderCommand, bool>(command, guid);

                _logger.LogInformation(
                    "----- Sending command: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
                    requestCancelOrder.GetGenericTypeName(),
                    nameof(requestCancelOrder.Command.OrderNumber),
                    requestCancelOrder.Command.OrderNumber,
                    requestCancelOrder);

                commandResult = await _mediator.Send(requestCancelOrder);
            }

            if (!commandResult)
            {
                return BadRequest();
            }

            return Ok();
        }

        [Route("ship")]
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ShipOrderAsync([FromBody] ShipOrderCommand command, [FromHeader(Name = "x-requestid")] string requestId)
        {
            bool commandResult = false;

            if (Guid.TryParse(requestId, out Guid guid) && guid != Guid.Empty)
            {
                var requestShipOrder = new IdentifiedCommand<ShipOrderCommand, bool>(command, guid);

                _logger.LogInformation(
                    "----- Sending command: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
                    requestShipOrder.GetGenericTypeName(),
                    nameof(requestShipOrder.Command.OrderNumber),
                    requestShipOrder.Command.OrderNumber,
                    requestShipOrder);

                commandResult = await _mediator.Send(requestShipOrder);
            }

            if (!commandResult)
            {
                return BadRequest();
            }

            return Ok();
        }

        [Route("{orderId:int}")]
        [HttpGet]
        [ProducesResponseType(typeof(Infrastructure.Models.Result.Order), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> GetOrderAsync(int orderId)
        {
            try
            {
                //Todo: It's good idea to take advantage of GetOrderByIdQuery and handle by GetCustomerByIdQueryHandler
                //var order customer = await _mediator.Send(new GetOrderByIdQuery(orderId));
                var order = await _orderService.GetOrderAsync(orderId);

                return Ok(order);
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<OrderSummary>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<OrderSummary>>> GetOrdersAsync()
        {
            var userid = _identityService.GetUserIdentity();
            var orders = await _orderService.GetOrdersFromUserAsync(Guid.Parse(userid));

            return Ok(orders);
        }

        [Route("cardtypes")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CardType>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<CardType>>> GetCardTypesAsync()
        {
            var cardTypes = await _orderService.GetCardTypesAsync();

            return Ok(cardTypes);
        }

        [Route("draft")]
        [HttpPost]
        public async Task<ActionResult<OrderDraftDTO>> CreateOrderDraftFromBasketDataAsync([FromBody] CreateOrderDraftCommand createOrderDraftCommand)
        {
            _logger.LogInformation(
                "----- Sending command: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
                createOrderDraftCommand.GetGenericTypeName(),
                nameof(createOrderDraftCommand.BuyerId),
                createOrderDraftCommand.BuyerId,
                createOrderDraftCommand);

            return await _mediator.Send(createOrderDraftCommand);
        }
    }
}
