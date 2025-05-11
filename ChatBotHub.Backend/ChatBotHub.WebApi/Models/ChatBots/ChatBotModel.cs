using ChatBotHub.WebApi.Models.Attachments;
using ChatBotHub.WebApi.Models.Common;

namespace ChatBotHub.WebApi.Models.ChatBots;

public record ChatBotModel : AuditableEntityModel {
    public required string Name { get; init; }
    public required string Description { get; init; }
    public required List<Guid> AttachmentsIds { get; init; }
}
