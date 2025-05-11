using ChatBotHub.Domain.Attachments;

namespace ChatBotHub.Application.Attachments;

public interface IAttachmentQueryService {
    Task<Attachment?> GetByIdAsync(Guid id);
    Task<Attachment> RequireAsync(Guid id);
    Task<List<Attachment>> GetByIdsAsync(List<Guid> ids);
    Task<List<Attachment>> GetAllAsync();
}
