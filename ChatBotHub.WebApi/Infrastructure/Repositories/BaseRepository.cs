using MongoDB.Driver;
using System.Linq.Expressions;
using ChatBotHub.WebApi.Infrastructure.Context;
using ChatBotHub.WebApi.Infrastructure.Domain;

namespace ChatBotHub.WebApi.Infrastructure.Repositories;


public class BaseRepository<T> : IBaseRepository<T> where T : BaseModel {
    protected readonly DataContext Context;
    protected readonly IMongoCollection<T> Collection;
    protected readonly string CollectionName;

    public BaseRepository(DataContext context, string collectionName) {
        Context = context;
        Collection = context.GetCollection<T>(collectionName);
        CollectionName = collectionName;
    }

    public async Task<Guid> CreateAsync(T entity) {
        entity.GenerateId();
        await Collection.InsertOneAsync(entity);
        return entity.Id;
    }

    public async Task DeleteAllAsync() {
        await Collection.DeleteManyAsync(FilterDefinition<T>.Empty);
    }

    public async Task DeleteAsync(Guid id) {
        await Collection.DeleteOneAsync(e => e.Id == id);
    }

    public async Task<bool> ExistsAsync(Guid id) {
        return await Collection.Find(e => e.Id == id).AnyAsync();
    }

    public async Task<IEnumerable<T>> GetAllAsync() {
        return await Collection.Find(FilterDefinition<T>.Empty).ToListAsync();
    }

    public async Task<T?> GetByIdAsync(Guid id) {
        return await Collection.Find(e => e.Id == id).FirstOrDefaultAsync();
    }

    public async Task<T> FindOneAsync(Expression<Func<T, bool>> filter) {
        return await Collection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task<List<T>> FindListAsync(Expression<Func<T, bool>> filter) {
        return await Collection.Find(filter).ToListAsync();
    }

    public async Task UpdateAsync(T entity) {
        await Collection.ReplaceOneAsync(e => e.Id == entity.Id, entity);
    }
}
