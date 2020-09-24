using GraphQL.Types;

namespace GraphQLGraphTypeFirstNestedTable.Models
{
    public partial class Certification
    {
        public long Id { get; set; }
        public long EmployeeId { get; set; }
        public string Title { get; set; }
        public int? Month { get; set; }
        public int? Year { get; set; }
        public string Provider { get; set; }
    }

}
