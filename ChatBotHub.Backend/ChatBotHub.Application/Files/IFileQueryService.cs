using File = ChatBotHub.Domain.Files.File;

namespace ChatBotHub.Application.Files;

public interface IFileQueryService {
    Task<File?> GetByIdAsync(Guid id);
    Task<File> RequireByIdAsync(Guid id);
    Task<List<File>> GetAllAsync();
}
