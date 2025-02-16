using MongoDB.Bson.Serialization.Attributes;

namespace ChatBotHub.WebApi.Infrastructure.Domain;

public class BaseModel {
    [BsonId]
    public Guid Id { get; set; }

    public void GenerateId() {
        Id = Guid.NewGuid();
    }
}
