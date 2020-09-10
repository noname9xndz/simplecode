using System;

namespace Order.Infrastructure.Domain.AggregatesModel.Entities
{
    public class ClientRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime Time { get; set; }
    }
}