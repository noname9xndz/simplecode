using CQRSTest.Domain.Base;

namespace CQRSTest.Domain.Customers.Events
{
    public class CustomerCreated : Event
    {
        public string Name { get; set; }
        public string Address { get; set; }
    }
}