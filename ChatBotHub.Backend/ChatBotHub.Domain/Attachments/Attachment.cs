using ChatBotHub.Domain.Attachments.Exceptions;
using ChatBotHub.Domain.Attachments.Specifications;
using ChatBotHub.Domain.Common.Models;

namespace ChatBotHub.Domain.Attachments;

public class Attachment : AuditableEntity {
    public Attachment(Guid fileId, string name, string description) {
        GenerateId();

        SetFileId(fileId);
        SetName(name);
        SetDescription(description);
    }

    public Guid FileId { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public bool Indexed { get; private set; }

    public void SetFileId(Guid id) {
        FileId = id;
        MakeDirty();
    }

    public void SetName(string name) {
        if (Name == name) {
            return;
        }

        var spec = new AttachmentNameLengthSpecification(name).IsSatisfiedBy(this);
        if (!spec.Value) {
            throw new InvalidAttachmentNameException(spec.Message);
        }
        
        Name = name;
        MakeDirty();
    }

    public void SetDescription(string description) {
        if (Description == description) {
            return;
        }

        var spec = new AttachmentDescriptionLengthValidity(description).IsSatisfiedBy(this);
        if (!spec.Value) {
            throw new InvalidAttachmentDescriptionException(spec.Message);
        }

        Description = description;
        MakeDirty();
    }

    public void MarkIndexed() {
        Indexed = true;
    }

    private void MakeDirty() {
        Indexed = false;
    }
}
