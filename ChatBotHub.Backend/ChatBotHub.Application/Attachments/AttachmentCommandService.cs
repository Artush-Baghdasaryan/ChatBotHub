using ChatBotHub.Application.Attachments.Exceptions;
using ChatBotHub.Application.Attachments.Requests;
using ChatBotHub.Domain.Attachments;

namespace ChatBotHub.Application.Attachments;

public class AttachmentCommandService : IAttachmentCommandService {
    private readonly IAttachmentRepository _repository;
    private readonly IAttachmentQueryService _queryService;

    public AttachmentCommandService(
        IAttachmentRepository repository,
        IAttachmentQueryService queryService
    ) {
        _repository = repository;
        _queryService = queryService;
    }

    public async Task<Attachment> CreateAsync(SaveAttachmentRequest request) {
        var attachment = new Attachment(request.FileId, request.Description);
        await _repository.InsertAsync(attachment);

        return attachment;
    }

    public async Task<Attachment> UpdateAsync(Guid id, SaveAttachmentRequest request) {
        var attachment = await _queryService.RequireAsync(id);
        
        attachment.SetFileId(request.FileId);
        attachment.SetDescription(request.Description);

        await _repository.UpdateAsync(attachment);
        return attachment;
    }

    public async Task DeleteAsync(Guid id) {
        if (!await _repository.ExistsAsync(id)) {
            throw new AttachmentIsNotFoundException(id);
        }

        await _repository.DeleteAsync(id);
    }
}
