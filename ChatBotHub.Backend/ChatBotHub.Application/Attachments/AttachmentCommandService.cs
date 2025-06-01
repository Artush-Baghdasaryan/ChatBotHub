using ChatBotHub.Application.Attachments.Exceptions;
using ChatBotHub.Application.Attachments.Requests;
using ChatBotHub.Application.Files;
using ChatBotHub.Domain.Attachments;

namespace ChatBotHub.Application.Attachments;

public class AttachmentCommandService : IAttachmentCommandService {
    private readonly IAttachmentRepository _repository;
    private readonly IAttachmentQueryService _queryService;
    private readonly IFileQueryService _fileQueryService;

    public AttachmentCommandService(
        IAttachmentRepository repository,
        IAttachmentQueryService queryService,
        IFileQueryService fileQueryService
    ) {
        _repository = repository;
        _queryService = queryService;
        _fileQueryService = fileQueryService;
    }

    public async Task<Attachment> CreateAsync(SaveAttachmentRequest request) {
        var file = await _fileQueryService.RequireByIdAsync(request.FileId);
        var attachment = new Attachment(
            request.FileId,
            file.FileName,
            request.Description
        );

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

    public Task MakeAttachmentsIndexed(List<Attachment> attachments) {
        foreach (var attachment in attachments) {
            attachment.MarkIndexed();
        }

        return _repository.UpdateBatchAsync(attachments);
    }

    public async Task DeleteAsync(Guid id) {
        if (!await _repository.ExistsAsync(id)) {
            throw new AttachmentIsNotFoundException(id);
        }

        await _repository.DeleteAsync(id);
    }
}
