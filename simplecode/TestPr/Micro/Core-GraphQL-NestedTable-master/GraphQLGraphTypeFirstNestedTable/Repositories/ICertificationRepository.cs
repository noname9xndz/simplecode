using System.Collections.Generic;
using System.Threading.Tasks;
using GraphQLGraphTypeFirstNestedTable.Models;

namespace GraphQLGraphTypeFirstNestedTable.Repositories
{
    public interface ICertificationRepository
    {
        Task<List<Certification>> GetCertificationByEmployee(long employeeId);
        Task<List<Certification>> GetAllCertification();
    }

}
