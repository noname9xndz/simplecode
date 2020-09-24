using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQLGraphTypeFirstNestedTable.Models;
using Microsoft.EntityFrameworkCore;

namespace GraphQLGraphTypeFirstNestedTable.Repositories
{
    public class CertificationRepository : ICertificationRepository
    {
        private readonly GraphQLDemoContext _context;
        public CertificationRepository(GraphQLDemoContext context)
        {
            _context = context;
        }

        public async Task<List<Certification>> GetCertificationByEmployee(long employeeId)
        {
            var res = await _context.Certification.Where(a => a.EmployeeId == employeeId).AsNoTracking().ToListAsync();
            return res;
        }

        public async Task<List<Certification>> GetAllCertification()
        {
            var res = await _context.Certification.AsNoTracking().ToListAsync();
            return res;
        }
    }

}
