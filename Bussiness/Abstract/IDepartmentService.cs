using Entity.DAO;
using Entity.DTO;
using Entity.Filter;
using Entity.Helper;

namespace Bussiness.Abstract
{
    public interface IDepartmentService
    {
        Task<Response<DepartmentDAO>> AddAsync(DepartmentDTO department);

        Task<Response<DepartmentDAO>> DeleteAsync(DepartmentDTO department);

        Task<Response<DepartmentDAO>> GetAsync(Guid id);

        Task<Response<List<DepartmentDAO>>> GetAllAsync(DepartmentFilter filter);

        Task<Response<DepartmentDAO>> UpdateAsync(DepartmentDTO department);
    }
}
