namespace Equinox.Application.EventSourcedNormalizers.Product
{
    public class ProductHistoryData : HistoryBase
    {
        public string Name { get; set; }
        public string Price { get; set; }
    }
}