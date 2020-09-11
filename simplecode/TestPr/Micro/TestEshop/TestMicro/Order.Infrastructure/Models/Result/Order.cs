using System;
using System.Collections.Generic;
using System.Text;

namespace Order.Infrastructure.Models.Result
{
    public class Order
    {
        public Order()
        {
            OrderItems = new List<OrderItem>();
        }
        public int OrderNumber { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }
        public List<OrderItem> OrderItems { get; set; }
        public decimal Total { get; set; }
    }
}
