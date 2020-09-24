using GraphQL.Types;
using GraphQLGraphTypeFirstNestedTable.Models;
using GraphQLGraphTypeFirstNestedTable.Repositories;

namespace GraphQLGraphTypeFirstNestedTable.GraphQL.Types
{
    /// <summary>
    /// https://graphql-dotnet.github.io/docs/getting-started/schema-types
    /// </summary>
    public class EmployeeType : ObjectGraphType<Employee>
    {
        public EmployeeType(ICertificationRepository certificationRepository)
        {
            Field(a => a.Id);
            Field(a => a.Name);
            Field(a => a.Email);
            Field(a => a.Mobile);
            Field(a => a.Company);
            Field(a => a.Address);
            Field(a => a.ShortDescription);
            Field(a => a.LongDescription);

            Field<ListGraphType<EmployeeCertificationType>>(
                "certifications",
                resolve: context => certificationRepository.GetCertificationByEmployee(context.Source.Id)
                );

        }
    }

}
