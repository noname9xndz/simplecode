﻿using Newtonsoft.Json;
using System;

namespace DShop.Messages.Commands.Orders
{
    public class CancelOrder : ICommand
    {
        public Guid Id { get; }
        public Guid CustomerId { get; }

        [JsonConstructor]
        public CancelOrder(Guid id, Guid customerId)
        {
            Id = id;
            CustomerId = customerId;
        }
    }
}
