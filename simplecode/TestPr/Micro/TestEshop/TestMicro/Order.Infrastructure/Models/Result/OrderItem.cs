using System;
using System.Collections.Generic;
using System.Text;

namespace Order.Infrastructure.Models.Result
{
    
    public class OrderItem
    {
        public string ProductName { get; set; }
        public int Units { get; set; }
        public double UnitPrice { get; set; }
        public string PictureUrl { get; set; }
    }
}
