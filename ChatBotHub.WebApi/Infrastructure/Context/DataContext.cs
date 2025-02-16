

using Infrastructure.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

namespace ChatBotHub.WebApi.Infrastructure.Context;

public class DataContext {

    private readonly IMongoDatabase _database;

    public DataContext(IOptions<DatabaseSettings> databaseSettings) {
        var client = new MongoClient(databaseSettings.Value.ConnectionString);
        _database = client.GetDatabase(databaseSettings.Value.DatabaseName);
    }

    public IMongoCollection<TEntity> GetCollection<TEntity>(string collection) {
        return _database.GetCollection<TEntity>(collection);
    }

    public static void ConfigureData() {
        BsonSerializer.RegisterIdGenerator(typeof(Guid), CombGuidGenerator.Instance);
        BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.CSharpLegacy));
    }
}
