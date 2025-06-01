namespace ChatBotHub.Application.Attachments.Requests;

public class SaveAttachmentRequest {
    public required Guid FileId { get; init; }
    public required string Name { get; init; }
    public required string Description { get; init; }
}
