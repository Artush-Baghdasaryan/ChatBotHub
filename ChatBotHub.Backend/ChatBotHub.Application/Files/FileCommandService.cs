using ChatBotHub.Application.Files.Exceptions;
using ChatBotHub.Domain.Files;
using Microsoft.AspNetCore.Http;
using File = ChatBotHub.Domain.Files.File;

namespace ChatBotHub.Application.Files;

public class FileCommandService : IFileCommandService {
    private readonly IFileRepository _repository;

    public FileCommandService(IFileRepository repository) {
        _repository = repository;
    }

    public async Task<File> CreateAsync(IFormFile file) {
        using var memoryStream = new MemoryStream();
        await file.CopyToAsync(memoryStream);
        
        var fileModel = new File {
            FileName = file.FileName,
            FileData = memoryStream.ToArray(),
            FileType = file.ContentType
        };

        await _repository.InsertAsync(fileModel);
        return fileModel;
    }

    public async Task DeleteAsync(Guid id) {
        if (!await _repository.ExistsAsync(id)) {
            throw new FileIsNotFoundException(id);
        }

        await _repository.DeleteAsync(id);
    }
}
