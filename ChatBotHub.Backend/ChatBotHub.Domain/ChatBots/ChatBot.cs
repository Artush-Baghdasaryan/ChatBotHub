using ChatBotHub.Domain.Common.Models;

namespace ChatBotHub.Domain.ChatBots;

public class ChatBot : AuditableEntity {
    public required Guid AccountId { get; set; }
    public required string Name { get; set; }
    public required List<Guid> AttachmentsIds { get; set; }
}
