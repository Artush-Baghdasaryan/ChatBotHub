using System.Linq.Expressions;
using ChatBotHub.WebApi.Infrastructure.Domain;
using ChatBotHub.WebApi.Infrastructure.Repositories;

namespace ChatBotHub.WebApi.Application.Services.Implementations;

public class BaseService<T> : IBaseService<T> where T : BaseModel {
    protected readonly IBaseRepository<T> Repository;

    protected BaseService(IBaseRepository<T> repository) {
        Repository = repository;
    }

    public async Task<T?> GetByIdAsync(Guid id) {
        return await Repository.GetByIdAsync(id);
    }

    public async Task<IEnumerable<T>> GetAllAsync() {
        return await Repository.GetAllAsync();
    }

    public async Task<Guid> CreateAsync(T entity) {
        return await Repository.CreateAsync(entity);
    }

    public async Task UpdateAsync(T entity) {
        await Repository.UpdateAsync(entity);
    }

    public async Task DeleteAsync(Guid id) {
        await Repository.DeleteAsync(id);
    }

    public async Task DeleteAllAsync() {
        await Repository.DeleteAllAsync();
    }

    public async Task<bool> ExistsAsync(Guid id) {
        return await Repository.ExistsAsync(id);
    }

    public async Task<List<T>> SearchEntitiesAsync(Expression<Func<T, bool>> predicate) {
        return await Repository.FindListAsync(predicate);
    }
} 