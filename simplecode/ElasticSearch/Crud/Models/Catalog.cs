using System.ComponentModel.DataAnnotations;

namespace ElasticSearchCrud.Models
{
    public class Catalog
    {
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }
    }
}