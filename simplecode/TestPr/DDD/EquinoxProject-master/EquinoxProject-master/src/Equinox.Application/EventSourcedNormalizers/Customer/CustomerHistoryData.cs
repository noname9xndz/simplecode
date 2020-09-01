namespace Equinox.Application.EventSourcedNormalizers.Customer
{
    public class CustomerHistoryData : HistoryBase
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string BirthDate { get; set; }
        
    }
}