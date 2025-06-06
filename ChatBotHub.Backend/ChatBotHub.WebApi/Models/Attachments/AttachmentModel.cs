using ChatBotHub.WebApi.Models.Common;

namespace ChatBotHub.WebApi.Models.Attachments;

public record AttachmentModel : AuditableEntityModel {
    public Guid FileId { get; init; }
    public required string Name { get; init; }
    public required string Description { get; init;}
    public required bool Indexed { get; init; }
}
