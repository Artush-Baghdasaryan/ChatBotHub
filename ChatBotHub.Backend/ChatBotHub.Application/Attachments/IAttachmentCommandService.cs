using ChatBotHub.Application.Attachments.Requests;
using ChatBotHub.Domain.Attachments;

namespace ChatBotHub.Application.Attachments;

public interface IAttachmentCommandService {
    Task<Attachment> CreateAsync(SaveAttachmentRequest request);
    Task<Attachment> UpdateAsync(Guid id, SaveAttachmentRequest request);
    Task DeleteAsync(Guid id);
}
