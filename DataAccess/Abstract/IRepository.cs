using Entity.Helper;
using System.Linq.Expressions;

namespace DataAccess.Abstract
{
    public interface IRepository<TEntity>
        where TEntity : class, new()
    {
        Task<Response<TEntity>> AddAsync(TEntity entity);

        Task<Response<TEntity>> DeleteAsync(TEntity entity);

        Task<Response<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter);

        Task<Response<List<TEntity>>> GetAllAsync(Expression<Func<TEntity, bool>> filter = null);

        Task<Response<TEntity>> UpdateAsync(TEntity entity);
    }
}
