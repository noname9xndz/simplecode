using CQRSTest.Domain.Customers.Events;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace CQRSTest.EventHandlers
{
    public class DomainEventHandler : INotificationHandler<CustomerCreated>
    {
        private readonly ILogger<DomainEventHandler> _logger;

        public DomainEventHandler(ILogger<DomainEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(CustomerCreated notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation(JsonSerializer.Serialize(notification));
            return Task.CompletedTask;
        }
    }
}