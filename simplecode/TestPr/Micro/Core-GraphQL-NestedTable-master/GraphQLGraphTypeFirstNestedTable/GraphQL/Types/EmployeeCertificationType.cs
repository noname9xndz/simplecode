using GraphQL.Types;
using GraphQLGraphTypeFirstNestedTable.Models;

namespace GraphQLGraphTypeFirstNestedTable.GraphQL.Types
{
    public class EmployeeCertificationType : ObjectGraphType<Certification>
    {
        public EmployeeCertificationType()
        {
            Field(t => t.Id);
            Field(t => t.Title);
            Field(t => t.Month, nullable: true);
            Field(t => t.Year, nullable: true);
            Field(t => t.Provider);
            Field(t => t.EmployeeId);
        }
    }

}
