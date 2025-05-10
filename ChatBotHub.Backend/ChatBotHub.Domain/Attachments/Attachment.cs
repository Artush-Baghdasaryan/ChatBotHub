using ChatBotHub.Domain.Attachments.Exceptions;
using ChatBotHub.Domain.Attachments.Specifications;
using ChatBotHub.Domain.Common.Models;

namespace ChatBotHub.Domain.Attachments;

public class Attachment : AuditableEntity {
    public Attachment(Guid fileId, string description) {
        GenerateId();

        SetFileId(fileId);
        SetDescription(description);
    }

    public Guid FileId { get; private set; }
    public string? Description { get; private set; }

    public void SetFileId(Guid id) {
        FileId = id;
    }

    public void SetDescription(string description) {
        if (Description == description) {
            return;
        }

        var spec = new AttachmentDescriptionSpecification(description).IsSatisfiedBy(this);
        if (!spec.Value) {
            throw new InvalidAttachmentDescriptionException(spec.Message);
        }

        Description = description;
    }
}
