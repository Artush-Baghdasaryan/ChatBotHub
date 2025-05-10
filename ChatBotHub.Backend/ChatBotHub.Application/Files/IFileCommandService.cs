
using Microsoft.AspNetCore.Http;
using File = ChatBotHub.Domain.Files.File;

namespace ChatBotHub.Application.Files;

public interface IFileCommandService {
    Task<File> CreateAsync(IFormFile file);
    Task DeleteAsync(Guid id);
}
