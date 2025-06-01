using System.Diagnostics.CodeAnalysis;
using ChatBotHub.Domain.Attachments;
using ChatBotHub.WebApi.Mappers.Common;
using ChatBotHub.WebApi.Models.Attachments;

namespace ChatBotHub.WebApi.Mappers.Attachments;

public static class AttachmentMapper {
    [return: NotNullIfNotNull(nameof(attachment))]
    public static AttachmentModel? Map(Attachment? attachment) {
        if (attachment is null) {
            return null;
        }

        return new AttachmentModel {
            Id = attachment.Id,
            Name = attachment.Name,
            Description = attachment.Description,
            Indexed = attachment.Indexed,
            Audit = AuditMapper.Map(attachment.Audit)
        };
    }
}
