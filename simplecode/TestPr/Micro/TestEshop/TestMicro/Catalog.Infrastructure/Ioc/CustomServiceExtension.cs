using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Text;
using Autofac;
using Catalog.Infrastructure.Context;
using Catalog.Infrastructure.Filters;
using Catalog.Infrastructure.Models.Base;
using Event.Bus.Services.Azure;
using Event.Bus.Services.Base.Implementation;
using Event.Bus.Services.Base.Interface;
using Event.Bus.Services.Implementation;
using Event.Bus.Services.Interface;
using Event.Bus.Services.Rabbit;
using Event.Bus.Services.Rabbit.Implementation;
using Event.Bus.Services.Rabbit.Interface;
using EventLogEF.Context;
using EventLogEF.Services.Implementation;
using EventLogEF.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.ServiceBus;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.HealthChecks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using RabbitMQ.Client;

namespace Catalog.Infrastructure.Ioc
{

    public static class CustomServiceExtension
    {

        public static IServiceCollection AddAppInsight(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddApplicationInsightsTelemetry(configuration);
            //services.AddApplicationInsightsKubernetesEnricher();

            return services;
        }

        public static IServiceCollection AddCustomMVC(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers(options =>
            {
                options.Filters.Add(typeof(HttpGlobalExceptionFilter));
            }).AddNewtonsoftJson();

            //                services.AddCors(options =>
            //                {
            //                    options.AddPolicy("CorsPolicy",
            //                        builder => builder
            //                        .SetIsOriginAllowed((host) => true)
            //                        .AllowAnyMethod()
            //                        .AllowAnyHeader()
            //                        .AllowCredentials());
            //                });

            return services;
        }

        public static IServiceCollection AddCustomHealthCheck(this IServiceCollection services, IConfiguration configuration)
        {
            var accountName = configuration.GetValue<string>("AzureStorageAccountName");
            var accountKey = configuration.GetValue<string>("AzureStorageAccountKey");

            var healthCheckBuilder = services.AddHealthChecks();

            healthCheckBuilder
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
                healthCheckBuilder.AddRabbitMQ(
                        $"amqp://{configuration["EventBusConnection"]}",
                        name: "catalog-rabbitmqbus-check",
                        tags: new string[] { "rabbitmqbus" });
            }

            return services;
        }

        public static IServiceCollection AddCustomDbContext(this IServiceCollection services, IConfiguration configuration)
        {
           // var typeName = Assembly.GetAssembly(typeof(CatalogStartUp)).GetName().Name;
            //var test23 = typeof(CatalogStartUp).GetTypeInfo().Assembly.GetName().Name;
           // var type = typeof(CatalogStartUp).Assembly.ExportedTypes;
            services.AddEntityFrameworkSqlServer()
                .AddDbContext<CatalogDbContext>(options =>
                {
                    options.UseSqlServer(configuration["ConnectionString"],
                         sqlOptions =>
                        {
                            sqlOptions.MigrationsAssembly(Assembly.GetAssembly(typeof(CatalogStartUp)).GetName().Name);
                            //Configuring Connection Resiliency: https://docs.microsoft.com/en-us/ef/core/miscellaneous/connection-resiliency 
                            sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                        });
                });

            services.AddDbContext<EventLogDbContext>(options =>
            {
                options.UseSqlServer(configuration["ConnectionString"],
                    sqlOptions =>
                    {
                        sqlOptions.MigrationsAssembly(Assembly.GetAssembly(typeof(CatalogStartUp)).GetName().Name);
                        sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                    });
            });

            return services;
        }

        public static IServiceCollection AddCustomOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<CatalogSettings>(configuration);
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var problemDetails = new ValidationProblemDetails(context.ModelState)
                    {
                        Instance = context.HttpContext.Request.Path,
                        Status = StatusCodes.Status400BadRequest,
                        Detail = "Please refer to the errors property for additional details."
                    };

                    return new BadRequestObjectResult(problemDetails)
                    {
                        ContentTypes = { "application/problem+json", "application/problem+xml" }
                    };
                };
            });

            return services;
        }

        public static IServiceCollection AddSwagger(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwaggerGen(options =>
            {
                options.DescribeAllEnumsAsStrings();
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Test Micro - Catalog HTTP API",
                    Version = "v1",
                    Description = "The Catalog Microservice HTTP API. This is a Data-Driven/CRUD microservice sample"
                });
            });

            return services;

        }

        public static IServiceCollection AddIntegrationServices(this IServiceCollection services, IConfiguration configuration)
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

        public static IServiceCollection AddEventBus(this IServiceCollection services, IConfiguration configuration)
        {
            var subscriptionClientName = configuration["SubscriptionClientName"];

            if (configuration.GetValue<bool>("AzureServiceBusEnabled"))
            {
                services.AddSingleton<IEventBus, AzureEventBusServiceBus>(sp =>
                {
                    var serviceBusPersisterConnection = sp.GetRequiredService<IAzureServiceBusPersisterConnection>();
                    var iLifetimeScope = sp.GetRequiredService<ILifetimeScope>();
                    var logger = sp.GetRequiredService<ILogger<AzureEventBusServiceBus>>();
                    var eventBusSubcriptionsManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();

                    return new AzureEventBusServiceBus(serviceBusPersisterConnection, logger,
                        eventBusSubcriptionsManager, subscriptionClientName, iLifetimeScope);
                });

            }
            else
            {
                services.AddSingleton<IEventBus, RabbitMQEventBusServiceBus>(sp =>
                {
                    var rabbitMQPersistentConnection = sp.GetRequiredService<IRabbitMQPersistentConnection>();
                    var iLifetimeScope = sp.GetRequiredService<ILifetimeScope>();
                    var logger = sp.GetRequiredService<ILogger<RabbitMQEventBusServiceBus>>();
                    var eventBusSubcriptionsManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();

                    var retryCount = 5;
                    if (!string.IsNullOrEmpty(configuration["EventBusRetryCount"]))
                    {
                        retryCount = int.Parse(configuration["EventBusRetryCount"]);
                    }

                    return new RabbitMQEventBusServiceBus(rabbitMQPersistentConnection, logger, iLifetimeScope, eventBusSubcriptionsManager, subscriptionClientName, retryCount);
                });
            }

            services.AddSingleton<IEventBusSubscriptionsManager, EventBusSubscriptionsManager>();

            // services.AddTransient<OrderStatusChangedToAwaitingValidationIntegrationEventHandler>();
            // services.AddTransient<OrderStatusChangedToPaidIntegrationEventHandler>();

            return services;
        }
    }
}

