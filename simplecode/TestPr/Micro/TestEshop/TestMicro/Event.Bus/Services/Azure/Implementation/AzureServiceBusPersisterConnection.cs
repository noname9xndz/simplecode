using System;
using System.Collections.Generic;
using System.Text;
using Event.Bus.Services.Interface;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Logging;

namespace Event.Bus.Services.Implementation
{
   
    public class AzureServiceBusPersisterConnection : IAzureServiceBusPersisterConnection
    {
        private readonly ILogger<AzureServiceBusPersisterConnection> _logger;
        private readonly ServiceBusConnectionStringBuilder _serviceBusConnectionStringBuilder;
        private ITopicClient _topicClient;

        bool _disposed;

        public AzureServiceBusPersisterConnection(ServiceBusConnectionStringBuilder serviceBusConnectionStringBuilder,
            ILogger<AzureServiceBusPersisterConnection> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            _serviceBusConnectionStringBuilder = serviceBusConnectionStringBuilder ??
                                                 throw new ArgumentNullException(nameof(serviceBusConnectionStringBuilder));
            _topicClient = new TopicClient(_serviceBusConnectionStringBuilder, RetryPolicy.Default);
        }

        public ServiceBusConnectionStringBuilder ServiceBusConnectionStringBuilder => _serviceBusConnectionStringBuilder;

        public ITopicClient CreateModel()
        {
            if (_topicClient.IsClosedOrClosing)
            {
                _topicClient = new TopicClient(_serviceBusConnectionStringBuilder, RetryPolicy.Default);
            }

            return _topicClient;
        }

        public void Dispose()
        {
            if (_disposed) return;

            _disposed = true;
        }
    }
}
