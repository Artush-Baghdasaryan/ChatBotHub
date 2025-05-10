using ChatBotHub.Domain.ChatBots.Exceptions;
using ChatBotHub.Domain.ChatBots.Specifications;
using ChatBotHub.Domain.Common.Models;

namespace ChatBotHub.Domain.ChatBots;

public class ChatBot : AuditableEntity {
    public ChatBot(Guid accountId) {
        GenerateId();

        AccountId = accountId;
    }

    public Guid AccountId { get; private set; }
    public string? Name { get; private set; }
    public string? Description { get; private set; }
    public List<Guid> AttachmentsIds { get; private set; } = [];

    public void SetName(string name) {
        if (Name == name) {
            return;
        }

        var spec = new ChatBotNameSpecification(name).IsSatisfiedBy(this);
        if (!spec.Value) {
            throw new InvalidChatBotNameException(spec.Message);
        }

        Name = name;
    }

    public void SetDescription(string description) {
        if (Description == description) {
            return;
        }

        var spec = new ChatBotDescriptionSpecification(description).IsSatisfiedBy(this);
        if (!spec.Value) {
            throw new InvalidChatBotDescriptionException(spec.Message);
        }

        Description = description;
    }

    public void AddAttachment(Guid id) {
        var spec = new ChatBotAddAttachmentSpecification(id).IsSatisfiedBy(this);
        if (!spec.Value) {
            throw new InvalidAttachmentException(spec.Message);
        }

        AttachmentsIds.Add(id);
    }

    public void RemoveAttachment(Guid id) {
        var spec = new ChatBotRemoveAttachmentSpecification(id).IsSatisfiedBy(this);
        if (!spec.Value) {
            throw new InvalidAttachmentException(spec.Message);
        }

        AttachmentsIds.Remove(id);
    }
}
