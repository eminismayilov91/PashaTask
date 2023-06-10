using Entity.DAO;
using Entity.Helper;
using System.Linq.Expressions;

namespace DataAccess.Abstract
{
    public interface IDepartmentDAL
    {
        Task<Response<DepartmentDAO>> AddAsync(DepartmentDAO entity);

        Task<Response<DepartmentDAO>> DeleteAsync(DepartmentDAO entity);

        Task<Response<DepartmentDAO>> GetAsync(Expression<Func<DepartmentDAO, bool>> filter);

        Task<Response<List<DepartmentDAO>>> GetAllAsync(Expression<Func<DepartmentDAO, bool>> filter = null);

        Task<Response<DepartmentDAO>> UpdateAsync(DepartmentDAO entity);
    }
}
