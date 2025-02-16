namespace ChatBotHub.WebApi.Infrastructure.Domain;

public class ChatBot : BaseModel {
    public required Guid UserId { get; set; }
    public required string Name { get; set; }
    public List<Guid> AttachmentsIds { get; set; } = new();
}
