namespace ChatBotHub.WebApi.Infrastructure.Domain;

public class Attachment : BaseModel {
    public required Guid FileId { get; set; }
    public required string Description { get; set; }
}
