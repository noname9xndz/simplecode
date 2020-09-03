using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using Catalog.Infrastructure.Models.Base;
using Event.Bus.Services.Implementation;
using Event.Bus.Services.Interface;
using Event.Bus.Services.Rabbit;
using Event.Bus.Services.Rabbit.Implementation;
using Event.Bus.Services.Rabbit.Interface;
using EventLogEF.Services.Implementation;
using EventLogEF.Services.Interface;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace Catalog.Infrastructure.Ioc.ServiceExtensions
{
    public static class AddIntegrationServices
    {
        public static IServiceCollection Register(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<Func<DbConnection, IEventLogEFService>>(
                sp => (DbConnection c) => new EventLogEFService(c));

           // services.AddTransient<ICatalogIntegrationEventService, CatalogIntegrationEventService>();

            if (configuration.GetValue<bool>("AzureServiceBusEnabled"))
            {
                services.AddSingleton<IAzureServiceBusPersisterConnection>(sp =>
                {
                    var settings = sp.GetRequiredService<IOptions<CatalogSettings>>().Value;
                    var logger = sp.GetRequiredService<ILogger<AzureServiceBusPersisterConnection>>();

                    var serviceBusConnection = new ServiceBusConnectionStringBuilder(settings.EventBusConnection);

                    return new AzureServiceBusPersisterConnection(serviceBusConnection, logger);
                });
            }
            else
            {
                services.AddSingleton<IRabbitMQPersistentConnection>(sp =>
                {
                    var settings = sp.GetRequiredService<IOptions<CatalogSettings>>().Value;
                    var logger = sp.GetRequiredService<ILogger<RabbitMQPersistentConnection>>();

                    var factory = new ConnectionFactory()
                    {
                        HostName = configuration["EventBusConnection"],
                        DispatchConsumersAsync = true
                    };

                    if (!string.IsNullOrEmpty(configuration["EventBusUserName"]))
                    {
                        factory.UserName = configuration["EventBusUserName"];
                    }

                    if (!string.IsNullOrEmpty(configuration["EventBusPassword"]))
                    {
                        factory.Password = configuration["EventBusPassword"];
                    }

                    var retryCount = 5;
                    if (!string.IsNullOrEmpty(configuration["EventBusRetryCount"]))
                    {
                        retryCount = int.Parse(configuration["EventBusRetryCount"]);
                    }

                    return new RabbitMQPersistentConnection(factory, logger, retryCount);
                });
            }

            return services;
        }
    }
}
