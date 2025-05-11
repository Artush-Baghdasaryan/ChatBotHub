using ChatBotHub.Application.Files.Exceptions;
using ChatBotHub.Domain.Files;
using File = ChatBotHub.Domain.Files.File;

namespace ChatBotHub.Application.Files;

public class FileQueryService : IFileQueryService {
    private readonly IFileRepository _repository;

    public FileQueryService(IFileRepository repository) {
        _repository = repository;
    }

    public Task<File?> GetByIdAsync(Guid id) {
        return _repository.GetByIdAsync(id);
    }

    public async Task<File> RequireByIdAsync(Guid id) {
        return await _repository.GetByIdAsync(id) ??
            throw new FileIsNotFoundException(id);
    }

    public Task<List<File>> GetAllAsync() {
        return _repository.GetAllAsync();
    }
}
