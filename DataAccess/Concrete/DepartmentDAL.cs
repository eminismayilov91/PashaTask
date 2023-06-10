using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.MsSql;
using Entity.DAO;

namespace DataAccess.Concrete
{
    public class DepartmentDAL : EfMsSqlRepositoryBase<DepartmentDAO>, IDepartmentDAL
    {
        public DepartmentDAL(MSSQLDBContext dbContext) : base(dbContext)
        {

        }
    }
}
