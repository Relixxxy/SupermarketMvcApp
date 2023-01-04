using System.Linq.Expressions;
using SupermarketApp.Models;

namespace SupermarketApp.Data.Repository
{
    public interface IRepository<TEntity>
        where TEntity : BaseEntity
    {
        Task CreateAsync(TEntity entity);
        Task<TEntity> FindByIdAsync(int id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<IEnumerable<TEntity>> GetAllAsync(Func<TEntity, bool> predicate);
        Task RemoveAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task<IEnumerable<TEntity>> GetAllWithIncludeAsync(params Expression<Func<TEntity, object>>[] includeProperties);
        Task<IEnumerable<TEntity>> GetAllWithIncludeAsync(
            Func<TEntity, bool> predicate,
            params Expression<Func<TEntity, object>>[] includeProperties);
    }
}
