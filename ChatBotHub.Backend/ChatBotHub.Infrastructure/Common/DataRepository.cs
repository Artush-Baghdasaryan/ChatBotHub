using System.Linq.Expressions;
using ChatBotHub.Domain.Common.Models;
using ChatBotHub.Domain.Common.Repositories;
using ChatBotHub.Infrastructure.MongoDb;
using MongoDB.Driver;

namespace ChatBotHub.Infrastructure.Common;

public class DataRepository<TEntity> : IRepository<TEntity> where TEntity : Entity {
    protected readonly MongoDbDataContext DataContext;
    protected readonly IMongoCollection<TEntity> Collection;

    public DataRepository(MongoDbDataContext context, string collectionName) {
        DataContext = context;
        Collection = context.GetCollection<TEntity>(collectionName);
    }
    
    public async Task<TEntity?> GetByIdAsync(Guid id) {
        return await Collection.Find(e => e.Id == id).FirstOrDefaultAsync();
    }

    public async Task<List<TEntity>> GetAllAsync() {
        return await Collection.Find(FilterDefinition<TEntity>.Empty).ToListAsync();
    }

    public async Task<TEntity?> FindOneAsync(Expression<Func<TEntity, bool>> filter) {
        return await Collection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task<List<TEntity>> FindAsync(Expression<Func<TEntity, bool>> filter) {
        return await Collection.Find(filter).ToListAsync();
    }

    public async Task AddAsync(TEntity entity) {
        if (entity.Id is null) {
            entity.GenerateId();
        }
        
        if (entity is AuditableEntity auditable) {
            auditable.Audit.PerformCreated();
        }

        await Collection.InsertOneAsync(entity).ConfigureAwait(false);
    }

    public async Task UpdateAsync(TEntity entity) {
        if (entity is AuditableEntity auditable) {
            auditable.Audit.PerformModified();
        }

        await Collection.ReplaceOneAsync(e => e.Id == entity.Id, entity).ConfigureAwait(false);
    }

    public async Task DeleteAsync(Guid id) {
        await Collection.DeleteOneAsync(e => e.Id == id).ConfigureAwait(false);
    }

    public async Task<bool> ExistsAsync(Guid id) {
        return await Collection.Find(e => e.Id == id).AnyAsync();
    }
}
