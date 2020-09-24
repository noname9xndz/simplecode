using System;
using GraphQL.Types;
using GraphQL.Utilities;
using GraphQLGraphTypeFirstNestedTable.GraphQL.Queries;

namespace GraphQLGraphTypeFirstNestedTable.GraphQL.Schemas
{
    public class EmployeeSchema : Schema
    {
        public EmployeeSchema(IServiceProvider provider) : base(provider)
        {
            Query = provider.GetRequiredService<EmployeeQuery>();
        }
    }

}
