using ChatBotHub.Application.Attachments.Exceptions;
using ChatBotHub.Domain.Attachments;

namespace ChatBotHub.Application.Attachments;

public class AttachmentQueryService : IAttachmentQueryService {
    private readonly IAttachmentRepository _repository;

    public AttachmentQueryService(IAttachmentRepository repository) {
        _repository = repository;
    }

    public Task<Attachment?> GetByIdAsync(Guid id) {
        return _repository.GetByIdAsync(id);
    }

    public async Task<Attachment> RequireAsync(Guid id) {
        return await _repository.GetByIdAsync(id) ??
            throw new AttachmentIsNotFoundException(id);
    }

    public Task<List<Attachment>> GetByIdsAsync(List<Guid> ids) {
        return _repository.GetByIdsAsync(ids);
    }

    public Task<List<Attachment>> GetAllAsync() {
        return _repository.GetAllAsync();
    }
}
