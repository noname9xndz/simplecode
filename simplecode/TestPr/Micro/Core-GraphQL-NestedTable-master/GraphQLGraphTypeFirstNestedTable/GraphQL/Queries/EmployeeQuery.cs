using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL;
using GraphQL.Types;
using GraphQLGraphTypeFirstNestedTable.GraphQL.Types;
using GraphQLGraphTypeFirstNestedTable.Models;
using GraphQLGraphTypeFirstNestedTable.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GraphQLGraphTypeFirstNestedTable.GraphQL.Queries
{
    public class EmployeeQuery : ObjectGraphType
    {
        public EmployeeQuery(IEmployeeRepository employeeRepository, ICertificationRepository certificationRepository)
        {
            Field<ListGraphType<EmployeeType>>(
                "employees",
                resolve: context => employeeRepository.GetEmployees()
                );

            Field<ListGraphType<EmployeeType>>(
                "searchemployees",
                arguments: new QueryArguments(new List<QueryArgument>
                {
                    //https://graphql-dotnet.github.io/docs/getting-started/schema-types
                    new QueryArgument<IntGraphType>
                    {
                        Name = "certificationid"
                    },
                    new QueryArgument<IntGraphType>
                    {
                        Name = "year"
                    },
                    new QueryArgument<StringGraphType>
                    {
                        Name = "name"
                    }
                }),
                resolve: context =>
                {
                    var res = BuidQuery(context, employeeRepository);
                    return res;
                }
            );
            Field<EmployeeType>(
                "employee",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IdGraphType>>
                {
                    Name = "id"
                }),
                resolve: context =>
                {
                    var id = context.GetArgument<int>("id");
                    return employeeRepository.GetEmployeeById(id);
                }
            );

            Field<ListGraphType<EmployeeCertificationType>>(
                "certification",
                resolve: context => certificationRepository.GetAllCertification()
            );

        }

      
        private List<Employee> BuidQuery(IResolveFieldContext<object> context, IEmployeeRepository employeeRepository)
        {
            var contextCurrent = employeeRepository.GetCurrentContext();
            var employees = contextCurrent.Employee.AsNoTracking();
            var certifications = contextCurrent.Certification.AsNoTracking();
            var query = employees.Join(certifications,
                e => e.Id,
                c => c.EmployeeId, (e, c) => new {e, c});

            var employeeId = context.GetArgument<int?>("certificationid");

            if (employeeId.HasValue)
            {
                if (employeeId.Value <= 0)
                {
                    context.Errors.Add(new ExecutionError("certificationId must be greater than zero!"));
                    return new List<Employee>();
                }

                query = query.Where(x=> x.c.Id == employeeId.Value);
            }

            var year = context.GetArgument<int?>("year");

            if (year.HasValue)
            {
                query = query.Where(x => x.c.Year == year.Value);
            }

            var name = context.GetArgument<string>("name");

            if (!string.IsNullOrWhiteSpace(name))
            {
                query = query.Where(x => x.e.Name == name);
            }

            return  query.Select(x =>x.e).ToList();
        }
    }

}
