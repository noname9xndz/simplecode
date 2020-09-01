﻿

using System;
using CompanyName.MyMeetings.Modules.Payments.Domain.SeedWork;

namespace CompanyName.MyMeetings.Modules.Payments.Domain.SubscriptionPayments.Events
{
    public class SubscriptionPaymentPaidDomainEvent : DomainEventBase
    {
        public SubscriptionPaymentPaidDomainEvent(Guid subscriptionPaymentId, string status)
        {
            SubscriptionPaymentId = subscriptionPaymentId;
            Status = status;
        }

        public Guid SubscriptionPaymentId { get; }

        public string Status { get; }
    }
}