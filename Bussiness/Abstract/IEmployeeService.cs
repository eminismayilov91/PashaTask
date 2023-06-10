using Entity.DAO;
using Entity.DTO;
using Entity.Filter;
using Entity.Helper;

namespace Bussiness.Abstract
{
    public interface IEmployeeService
    {
        Task<Response<EmployeeDAO>> AddAsync(EmployeeDTO employee);

        Task<Response<EmployeeDAO>> DeleteAsync(EmployeeDTO employee);

        Task<Response<EmployeeDAO>> GetAsync(Guid id);

        Task<Response<EmployeeDTO>> GetEmployeeDetailedInfoByIdAsync(Guid id);

        Task<Response<List<EmployeeDAO>>> GetAllAsync(EmployeeFilter filter);

        Task<Response<EmployeeDAO>> UpdateAsync(EmployeeDTO employee);
    }
}
