using ChatBotHub.Application.AiKnowledge.Models;
using ChatBotHub.Domain.Attachments;

namespace ChatBotHub.Application.AiKnowledge.Mappers;

public static class ExternalAttachmentModelMapper {
    public static ExternalAttachmentModel Map(Attachment attachment) {
        return new ExternalAttachmentModel {
            Id = attachment.Id,
            Description = attachment.Description,
        };
    }
}
