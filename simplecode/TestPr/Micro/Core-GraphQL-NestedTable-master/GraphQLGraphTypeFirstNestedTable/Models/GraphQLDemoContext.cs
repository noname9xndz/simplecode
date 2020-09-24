using Microsoft.EntityFrameworkCore;

namespace GraphQLGraphTypeFirstNestedTable.Models
{
    public partial class GraphQLDemoContext : DbContext
    {
        public GraphQLDemoContext()
        {
        }

        public GraphQLDemoContext(DbContextOptions<GraphQLDemoContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<Certification> Certification { get; set; }
    }

}
