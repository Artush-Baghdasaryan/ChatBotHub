using ChatBotHub.Infrastructure.Common;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

namespace ChatBotHub.Infrastructure.MongoDb;

public class MongoDbDataContext {
    private readonly IMongoDatabase _database;

    public MongoDbDataContext(IOptions<DatabaseOptions> databaseOptions) {
        var client = new MongoClient(databaseOptions.Value.ConnectionString);
        _database = client.GetDatabase(databaseOptions.Value.DatabaseName);
    }

    public IMongoCollection<TEntity> GetCollection<TEntity>(string collection) {
        return _database.GetCollection<TEntity>(collection);
    }

    public static void ConfigureData() {
        BsonSerializer.RegisterIdGenerator(typeof(Guid), CombGuidGenerator.Instance);
        BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.CSharpLegacy));
    }
}
