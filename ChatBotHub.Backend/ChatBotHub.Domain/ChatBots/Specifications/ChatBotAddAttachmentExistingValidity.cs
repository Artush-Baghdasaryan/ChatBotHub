using ChatBotHub.Domain.Common.Specifications;

namespace ChatBotHub.Domain.ChatBots.Specifications;

public class ChatBotAddAttachmentExistingValidity : ISpecification<ChatBot> {
    private readonly Guid _attachmentId;

    public ChatBotAddAttachmentExistingValidity(Guid attachmentId) {
        _attachmentId = attachmentId;
    }

    public SpecificationResult IsSatisfiedBy(ChatBot bot) {
        if (bot.AttachmentsIds.Contains(_attachmentId)) {
            return SpecificationResult.False(
                $"Attachment with id ${_attachmentId} already exists for bot with id ${bot.Id}");
        }

        return true;
    }
}
