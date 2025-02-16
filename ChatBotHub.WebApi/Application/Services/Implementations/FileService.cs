using ChatBotHub.WebApi.Infrastructure.Domain;
using ChatBotHub.WebApi.Infrastructure.Repositories;

namespace ChatBotHub.WebApi.Application.Services.Implementations;

public class FileService : BaseService<FileModel>, IFileService {
    private readonly FileModelRepository _fileModelRepository;
    private readonly AttachmentRepository _attachmentRepository;
    
    public FileService(
        FileModelRepository fileModelRepository,
        AttachmentRepository attachmentRepository
    ) : base(fileModelRepository) {
        _fileModelRepository = fileModelRepository;
        _attachmentRepository = attachmentRepository;
    }

    public async Task<Guid> UploadFileAsync(IFormFile file) {
        using var memoryStream = new MemoryStream();
        await file.CopyToAsync(memoryStream);
        var fileModel = new FileModel {
            FileName = file.FileName,
            FileData = memoryStream.ToArray(),
            FileType = file.ContentType,
            UploadDate = DateTime.Now,
        };

        return await _fileModelRepository.CreateAsync(fileModel);
    }
}