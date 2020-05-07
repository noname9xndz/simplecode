using CQRSTest.Domain.Customers;
using System.Collections.Generic;

namespace CQRSTest.DataAccess
{
    public class FakeDbContext
    {
        public static List<Customer> Customers { get; set; } = new List<Customer>()
            {
                new Customer("Tuan Nguyen","Vietnam" ),
                new Customer("Nguyen Tuan","Vietnam" )
            };
    }
}