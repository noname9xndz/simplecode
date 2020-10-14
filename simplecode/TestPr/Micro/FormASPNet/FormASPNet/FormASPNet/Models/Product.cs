using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FormASPNet.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }
        public byte[] Image { get; set; }
        public string UrlImage { get; set; }
    }
}