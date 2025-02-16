using ChatBotHub.WebApi.Dto;
using ChatBotHub.WebApi.Infrastructure.Domain;

namespace ChatBotHub.WebApi.Application.Services;

public interface IChatBotService : IBaseService<ChatBot> {
    public Task<Guid> CreateBotAsync(Guid userId, string name, List<FileUploadDto> files);
}
