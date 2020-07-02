using System;

namespace ProductElasticSearchAdvanced.Models
{
    public class Review
    {
        public int Id { get; set; }
        public float Rating { get; set; }
        public string Description { get; set; }
        public User User { get; set; }
        public DateTime Date { get; set; }
    }
}