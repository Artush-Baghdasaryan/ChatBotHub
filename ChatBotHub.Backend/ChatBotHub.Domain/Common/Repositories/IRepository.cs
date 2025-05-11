using System.Linq.Expressions;
using ChatBotHub.Domain.Common.Models;
using MongoDB.Driver;

namespace ChatBotHub.Domain.Common.Repositories;

public interface IRepository<TEntity> where TEntity : Entity {
    Task<TEntity?> GetByIdAsync(Guid id);
    Task<List<TEntity>> GetByIdsAsync(List<Guid> ids);
    Task<List<TEntity>> GetAllAsync();
    Task<TEntity?> FindOneAsync(Expression<Func<TEntity, bool>> filter);
    Task<List<TEntity>> FindAsync(Expression<Func<TEntity, bool>> filter);
    Task InsertAsync(TEntity entity);
    Task InsertBatchAsync(List<TEntity> entities);
    Task UpdateAsync(TEntity entity);
    Task DeleteAsync(Guid id);
    Task<bool> ExistsAsync(Guid id);
}
