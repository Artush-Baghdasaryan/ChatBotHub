using ChatBotHub.Application.Attachments.Requests;
using ChatBotHub.Domain.Attachments;

namespace ChatBotHub.Application.Attachments;

public interface IAttachmentCommandService {
    Task<Attachment> CreateAsync(Guid botId, SaveAttachmentRequest request);
    Task<Attachment> UpdateAsync(Guid id, Guid botId, SaveAttachmentRequest request);
    Task DeleteAsync(Guid id);
}
