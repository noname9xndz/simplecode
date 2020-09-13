﻿using Microsoft.Extensions.DependencyInjection;
using RawRabbit.Instantiation;
using RawRabbit.DependencyInjection.ServiceCollection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RawRabbit;
using MediatR;
using Microsoft.AspNetCore.Builder;

namespace PaymentService.Messaging.RabbitMq
{
    public static class RawRabbitInstaller
    {
        public static IServiceCollection AddRabbitListeners(this IServiceCollection services)
        {
            services.AddRawRabbit(new RawRabbitOptions
            {
                ClientConfiguration = new RawRabbit.Configuration.RawRabbitConfiguration
                {
                    Username = "guest",
                    Password = "guest",
                    VirtualHost = "/",
                    Port = 5672,
                    Hostnames = new List<string> { "localhost" },
                    RequestTimeout = TimeSpan.FromSeconds(10),
                    PublishConfirmTimeout = TimeSpan.FromSeconds(1),
                    RecoveryInterval = TimeSpan.FromSeconds(1),
                    PersistentDeliveryMode = true,
                    AutoCloseConnection = true,
                    AutomaticRecovery = true,
                    TopologyRecovery = true,
                    Exchange = new RawRabbit.Configuration.GeneralExchangeConfiguration
                    {
                        Durable = true,
                        AutoDelete = false,
                        Type = RawRabbit.Configuration.Exchange.ExchangeType.Topic
                    },
                    Queue = new RawRabbit.Configuration.GeneralQueueConfiguration
                    {
                        Durable = true,
                        AutoDelete = false,
                        Exclusive = false
                    }
                }
            });

            services.AddSingleton(svc => new RabbitEventListener(svc.GetRequiredService<IBusClient>(), svc));
            
            return services;
        }
    }

    public static class RabbitListenersInstaller
    {
        public static void UseRabbitListeners(this IApplicationBuilder app, List<Type> eventTypes)
        {
            app.ApplicationServices.GetRequiredService<RabbitEventListener>().ListenTo(eventTypes);
        }
    }
}
