using ChatBotHub.WebApi.Infrastructure.Domain;

namespace ChatBotHub.WebApi.Application.Services;

public interface IFileService : IBaseService<FileModel> {
    Task<Guid> UploadFileAsync(IFormFile file);
}
