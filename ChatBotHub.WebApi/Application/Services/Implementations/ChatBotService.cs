using ChatBotHub.WebApi.Dto;
using ChatBotHub.WebApi.Infrastructure.Domain;
using ChatBotHub.WebApi.Infrastructure.Repositories;

namespace ChatBotHub.WebApi.Application.Services.Implementations;

public class ChatBotService : BaseService<ChatBot>, IChatBotService {
    private IFileService _fileService;
    private AttachmentRepository _attachmentRepository;
    
    public ChatBotService(
        ChatBotRepository repository,
        IFileService fileService, AttachmentRepository attachmentRepository) : base(repository) {
        _fileService = fileService;
        _attachmentRepository = attachmentRepository;
    }

    public async Task<Guid> CreateBotAsync(Guid userId, string name, List<FileUploadDto> files) {
        var attachmentsIds = new List<Guid>();

        foreach (var file in files) {
            var fileId = await _fileService.UploadFileAsync(file.File);
            var attachment = new Attachment {
                FileId = fileId,
                Description = file.Description
            };

            var attachmentId = await _attachmentRepository.CreateAsync(attachment);
            attachmentsIds.Add(attachmentId);
        }

        var chatBot = new ChatBot {
            UserId = userId,
            Name = name,
            AttachmentsIds = attachmentsIds
        };

        return await Repository.CreateAsync(chatBot);
    }
}
