using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQLGraphTypeFirstNestedTable.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace GraphQLGraphTypeFirstNestedTable.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly GraphQLDemoContext _context;
        public EmployeeRepository(GraphQLDemoContext context)
        {
            _context = context;
        }

        public async Task<List<Employee>> GetEmployees()
        {
            var res = await _context.Employee.AsNoTracking().ToListAsync();
            return res;
        }

        public async Task<Employee> GetEmployeeById(long id)
        {
            var res = await _context.Employee.AsNoTracking().SingleOrDefaultAsync(a => a.Id == id);
            return res;
        }

        public GraphQLDemoContext GetCurrentContext()
        {
            return _context;
        }
    }

}
