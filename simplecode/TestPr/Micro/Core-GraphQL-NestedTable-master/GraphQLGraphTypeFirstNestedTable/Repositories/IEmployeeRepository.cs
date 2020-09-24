using System.Collections.Generic;
using System.Threading.Tasks;
using GraphQLGraphTypeFirstNestedTable.Models;
using Microsoft.EntityFrameworkCore.Query;

namespace GraphQLGraphTypeFirstNestedTable.Repositories
{
    public interface IEmployeeRepository
    {
        Task<List<Employee>> GetEmployees();

        Task<Employee> GetEmployeeById(long id);
        GraphQLDemoContext GetCurrentContext();
    }

}
