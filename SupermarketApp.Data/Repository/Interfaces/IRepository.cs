using System.Linq.Expressions;
using SupermarketApp.Data.Entities;

namespace SupermarketApp.Data.Repository.Interfaces
{
    public interface IRepository<TEntity>
        where TEntity : BaseEntity
    {
        Task CreateAsync(TEntity entity);
        Task<TEntity> FindByIdAsync(int id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task RemoveAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
    }
}
