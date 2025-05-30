﻿using System.Linq.Expressions;
using ChatBotHub.Domain.Common.Models;
using ChatBotHub.Domain.Common.Repositories;
using ChatBotHub.Infrastructure.MongoDb;
using MongoDB.Driver;

namespace ChatBotHub.Infrastructure.Common;

public class DataRepository<TEntity> : IRepository<TEntity> where TEntity : Entity {
    protected readonly MongoDbDataContext DataContext;
    protected readonly IMongoCollection<TEntity> Collection;

    protected DataRepository(MongoDbDataContext context, string collectionName) {
        DataContext = context;
        Collection = context.GetCollection<TEntity>(collectionName);
    }
    
    public async Task<TEntity?> GetByIdAsync(Guid id) {
        return await Collection.Find(e => e.Id == id).FirstOrDefaultAsync();
    }

    public async Task<List<TEntity>> GetByIdsAsync(List<Guid> ids) {
        return await Collection.Find(e => ids.Contains(e.Id)).ToListAsync();
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

    public async Task InsertAsync(TEntity entity) {
        InitDataOnCreate(entity);
        await Collection.InsertOneAsync(entity).ConfigureAwait(false);
    }

    public async Task InsertBatchAsync(List<TEntity> entities) {
        foreach (var entity in entities) {
            InitDataOnCreate(entity);
        }

        await Collection.InsertManyAsync(entities);
    }

    private static void InitDataOnCreate(TEntity entity) {
        if (entity.Id == Guid.Empty) {
            entity.GenerateId();
        }

        if (entity is AuditableEntity auditable) {
            auditable.Audit.PerformCreated();
        }
    }

    public async Task UpdateAsync(TEntity entity) {
        InitDataOnUpdate(entity);
        await Collection.ReplaceOneAsync(e => e.Id == entity.Id, entity).ConfigureAwait(false);
    }

    public async Task UpdateBatchAsync(List<TEntity> entities) {
        var updates = new List<WriteModel<TEntity>>();

        foreach (var entity in entities) {
            InitDataOnUpdate(entity);
            
            var filter = Builders<TEntity>.Filter.Where(x => x.Id == entity.Id);
            updates.Add(new ReplaceOneModel<TEntity>(filter, entity));
        }

        if (updates.Any()) {
            await Collection.BulkWriteAsync(updates);
        }
    }

    private void InitDataOnUpdate(TEntity entity) {
        if (entity is AuditableEntity auditableEntity) {
            auditableEntity.Audit.PerformModified();
        }
    }

    public async Task DeleteAsync(Guid id) {
        await Collection.DeleteOneAsync(e => e.Id == id).ConfigureAwait(false);
    }

    public async Task<bool> ExistsAsync(Guid id) {
        return await Collection.Find(e => e.Id == id).AnyAsync();
    }
}
