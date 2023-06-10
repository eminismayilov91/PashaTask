using DataAccess.Abstract;
using Entity.Helper;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DataAccess.Concrete.EntityFramework.MsSql
{
    public class EfMsSqlRepositoryBase<TEntity> : IRepository<TEntity>
        where TEntity : class, new()
    {
        protected readonly MSSQLDBContext _dbContext;
        public EfMsSqlRepositoryBase(MSSQLDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Response<TEntity>> AddAsync(TEntity entity)
        {
            try
            {
                using (_dbContext)
                {
                    var addedEntity = _dbContext.Entry(entity);
                    addedEntity.State = EntityState.Added;
                    await _dbContext.SaveChangesAsync();
                    return Response<TEntity>.Succeed(entity);
                }
            }
            catch (Exception exception)
            {
                return Response<TEntity>.Failed("", exception);
            }
        }

        public async Task<Response<TEntity>> DeleteAsync(TEntity entity)
        {
            try
            {
                using (_dbContext)
                {
                    var deletedEntity = _dbContext.Entry(entity);
                    deletedEntity.State = EntityState.Deleted;
                    await _dbContext.SaveChangesAsync();
                    return Response<TEntity>.Succeed(entity);
                }
            }
            catch (Exception exception)
            {
                return Response<TEntity>.Failed("", exception);
            }
        }

        public async Task<Response<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter)
        {
            try
            {
                using (_dbContext)
                {
                    var result = await _dbContext.Set<TEntity>().FirstOrDefaultAsync(filter);
                    return Response<TEntity>.Succeed(result);
                }
            }
            catch (Exception exception)
            {
                return Response<TEntity>.Failed("", exception);
            }

        }

        public async Task<Response<List<TEntity>>> GetAllAsync(Expression<Func<TEntity, bool>> filter = null)
        {
            try
            {
                using (_dbContext)
                {
                    var result = await (filter == null ? _dbContext.Set<TEntity>().ToListAsync() : _dbContext.Set<TEntity>().Where(filter).ToListAsync());
                    return Response<List<TEntity>>.Succeed(result);
                }
            }
            catch (Exception exception)
            {
                return Response<List<TEntity>>.Failed("", exception);
            }
        }

        public async Task<Response<TEntity>> UpdateAsync(TEntity entity)
        {
            try
            {
                using (_dbContext)
                {
                    var updatedEntity = _dbContext.Entry(entity);
                    updatedEntity.State = EntityState.Modified;
                    await _dbContext.SaveChangesAsync();
                    return Response<TEntity>.Succeed(entity);
                }
            }
            catch (Exception exception)
            {
                return Response<TEntity>.Failed("", exception);
            }
        }
    }
}
