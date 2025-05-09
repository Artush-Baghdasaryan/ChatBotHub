using ChatBotHub.Domain.Common.Models;

namespace ChatBotHub.Domain.Attachments;

public class Attachment : AuditableEntity {
    public required Guid FileId { get; set; }
    public required Guid Description { get; set; }
}
