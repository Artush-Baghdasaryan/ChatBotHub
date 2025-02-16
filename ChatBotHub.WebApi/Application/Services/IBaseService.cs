using System.Linq.Expressions;
using ChatBotHub.WebApi.Infrastructure.Domain;

namespace ChatBotHub.WebApi.Application.Services;

public interface IBaseService<T> where T : BaseModel {
    Task<T?> GetByIdAsync(Guid id);
    Task<IEnumerable<T>> GetAllAsync();
    Task<Guid> CreateAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(Guid id);
    Task DeleteAllAsync();
    Task<bool> ExistsAsync(Guid id);
    Task<List<T>> SearchEntitiesAsync(Expression<Func<T, bool>> predicate);
}