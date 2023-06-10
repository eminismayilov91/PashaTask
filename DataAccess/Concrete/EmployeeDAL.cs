using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.MsSql;
using Entity.DAO;
using Entity.DTO;
using Entity.Helper;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete
{
    public class EmployeeDAL : EfMsSqlRepositoryBase<EmployeeDAO>, IEmployeeDAL
    {
        public EmployeeDAL(MSSQLDBContext dbContext):base(dbContext)
        {
                
        }
        public async Task<Response<EmployeeDTO>> GetEmployeeDetailedInfoAsync(Guid id)
        {
            try
            {
                using (_dbContext)
                {
                    var result = await (from employee in _dbContext.Employees
                                        join department in _dbContext.Departments on employee.DepartmentId equals department.Id
                                        where employee.Id == id
                                        select new EmployeeDTO
                                        {
                                            Id = employee.Id,
                                            DepartmentId = employee.Id,
                                            BirthDate = employee.BirthDate,
                                            CreateDate = employee.CreateDate,
                                            DepartmentName = department.Name,
                                            Name = employee.Name,
                                            Surname = employee.Surname,
                                        }).FirstOrDefaultAsync();
                    return Response<EmployeeDTO>.Succeed(result);
                }
            }
            catch (Exception exception)
            {
                return Response<EmployeeDTO>.Failed("", exception);
            }
        }
    }
}
