using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQLGraphTypeFirstNestedTable.Models;
using GraphQLGraphTypeFirstNestedTable.Models.Responses;

namespace GraphQLGraphTypeFirstNestedTable.GraphQL.Clients
{
    public interface IEmployeeGraphqlClient
    {
        Task<ResponseModel<List<Employee>>> GetAllEmployeeAsync();

        Task<ResponseModel<Employee>> GetEmployeeByIdAsync(int id);

        Task<ResponseModel<List<Employee>>> SearchEmployeeAsync();
    }
}
