using System.Linq.Expressions;
using ChatBotHub.WebApi.Infrastructure.Domain;

namespace ChatBotHub.WebApi.Infrastructure.Repositories;

public interface IBaseRepository<T> where T : BaseModel {
    Task<Guid> CreateAsync(T entity);
    Task<T?> GetByIdAsync(Guid id);
    Task<IEnumerable<T>> GetAllAsync();
    Task UpdateAsync(T entity);
    Task<bool> ExistsAsync(Guid id);
    Task DeleteAsync(Guid id);
    Task DeleteAllAsync();
    Task<T> FindOneAsync(Expression<Func<T, bool>> filter);
    Task<List<T>> FindListAsync(Expression<Func<T, bool>> filter);
}
