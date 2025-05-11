using ChatBotHub.Domain.Common.Specifications;

namespace ChatBotHub.Domain.ChatBots.Specifications;

public class ChatBotRemoveAttachmentExistingValidity : ISpecification<ChatBot> {
    private readonly Guid _attachmentId;

    public ChatBotRemoveAttachmentExistingValidity(Guid attachmentId) {
        _attachmentId = attachmentId;
    }

    public SpecificationResult IsSatisfiedBy(ChatBot entity) {
        if (!entity.AttachmentsIds.Contains(_attachmentId)) {
            return SpecificationResult.False(
                $"Attachment with id {_attachmentId} does not exist for bot with id {entity.Id}");
        }

        return true;
    }
}
