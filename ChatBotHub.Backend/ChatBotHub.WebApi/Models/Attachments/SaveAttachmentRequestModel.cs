namespace ChatBotHub.WebApi.Models.Attachments;

public record SaveAttachmentRequestModel {
    public required Guid FileId { get; init; }
    public required string Description { get; init; }
}
