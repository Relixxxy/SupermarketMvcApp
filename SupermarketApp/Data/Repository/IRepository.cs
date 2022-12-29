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
    }
}
