﻿using System;
using System.Collections.Generic;

namespace DShop.Monolith.Core.Domain.Orders
{
    public class Order : AggregateRoot
    {
        public Guid CustomerId { get; protected set; }
        public long Number { get; protected set; }
        public IEnumerable<Guid> ProductIds { get; protected set; }
        public decimal TotalAmount { get; protected set; }
        public string Currency { get; protected set; }
        public OrderStatus Status { get; protected set; }

        public Order(Guid id, Guid customerId, long number, IEnumerable<Guid> productIds, 
            decimal totalAmount, string currency) : base(id)
        {
            CustomerId = customerId;
            Number = number;
            ProductIds = productIds;
            TotalAmount = totalAmount;
            Currency = currency;
            Status = OrderStatus.Created;
        }

        public void Complete()
        {
            if(Status == OrderStatus.Canceled)
            {
                throw new DomainException("Cannot complete canceled order.");
            }
            Status = OrderStatus.Completed;
        }

        public void Cancel()
        {
            if (Status == OrderStatus.Completed)
            {
                throw new DomainException("Cannot cancel completed order.");
            }
            Status = OrderStatus.Canceled;
        }

        public enum OrderStatus : byte
        {
            Created = 0,
            Completed = 1,
            Canceled = 2,
        }
    }
}
