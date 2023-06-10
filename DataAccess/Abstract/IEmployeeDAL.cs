using Entity.DAO;
using Entity.DTO;
using Entity.Helper;
using System.Linq.Expressions;

namespace DataAccess.Abstract
{
    public interface IEmployeeDAL
    {
        Task<Response<EmployeeDAO>> AddAsync(EmployeeDAO entity);

        Task<Response<EmployeeDAO>> DeleteAsync(EmployeeDAO entity);

        Task<Response<EmployeeDAO>> GetAsync(Expression<Func<EmployeeDAO, bool>> filter);

        Task<Response<List<EmployeeDAO>>> GetAllAsync(Expression<Func<EmployeeDAO, bool>> filter = null);

        Task<Response<EmployeeDAO>> UpdateAsync(EmployeeDAO entity);

        Task<Response<EmployeeDTO>> GetEmployeeDetailedInfoAsync(Guid id);
    }
}
