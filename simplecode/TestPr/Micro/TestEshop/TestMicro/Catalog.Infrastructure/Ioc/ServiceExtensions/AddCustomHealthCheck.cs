using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.HealthChecks;

namespace Catalog.Infrastructure.Ioc.ServiceExtensions
{
    public static class AddCustomHealthCheck
    {
        public static IServiceCollection Register(this IServiceCollection services, IConfiguration configuration)
        {
            var accountName = configuration.GetValue<string>("AzureStorageAccountName");
            var accountKey = configuration.GetValue<string>("AzureStorageAccountKey");

            var hcBuilder = services.AddHealthChecks();

            hcBuilder
               // .AddCheck("self", () => HealthCheckResult.Healthy())
                .AddSqlServer(
                    configuration["ConnectionString"],
                    name: "CatalogDB-check",
                    tags: new string[] { "catalogdb" });

            if (!string.IsNullOrEmpty(accountName) && !string.IsNullOrEmpty(accountKey))
            {
//                hcBuilder
//                    .AddAzureBlobStorage(
//                        $"DefaultEndpointsProtocol=https;AccountName={accountName};AccountKey={accountKey};EndpointSuffix=core.windows.net",
//                        name: "catalog-storage-check",
//                        tags: new string[] { "catalogstorage" });
            }

            if (configuration.GetValue<bool>("AzureServiceBusEnabled"))
            {
//                hcBuilder
//                    .AddAzureServiceBusTopic(
//                        configuration["EventBusConnection"],
//                        topicName: "eshop_event_bus",
//                        name: "catalog-servicebus-check",
//                        tags: new string[] { "servicebus" });
            }
            else
            {
                hcBuilder
                    .AddRabbitMQ(
                        $"amqp://{configuration["EventBusConnection"]}",
                        name: "catalog-rabbitmqbus-check",
                        tags: new string[] { "rabbitmqbus" });
            }

            return services;
        }
    }
}
