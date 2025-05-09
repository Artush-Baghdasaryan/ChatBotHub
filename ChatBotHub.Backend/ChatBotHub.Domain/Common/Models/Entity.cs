using MongoDB.Bson.Serialization.Attributes;

namespace ChatBotHub.Domain.Common.Models;

public class Entity {
    [BsonId]
    public Guid? Id { get; protected set; }

    public void SetId(Guid id) {
        Id = id;
    }

    public void GenerateId() {
        Id = Guid.NewGuid();
    }
}
