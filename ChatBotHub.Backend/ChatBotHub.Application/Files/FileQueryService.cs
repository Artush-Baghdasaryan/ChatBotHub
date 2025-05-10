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

    public Task<List<File>> GetAllAsync() {
        return _repository.GetAllAsync();
    }
}
