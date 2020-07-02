namespace ProductElasticSearch
{
    public class ProductSettings
    {
        public string Name { get; set; } = "ProductElasticsearch";
        public string Description { get; set; } = "A short description of the site";
        public string Owner { get; set; } = "noname";
        public int ProductsPerPage { get; set; } = 50;
    }
}