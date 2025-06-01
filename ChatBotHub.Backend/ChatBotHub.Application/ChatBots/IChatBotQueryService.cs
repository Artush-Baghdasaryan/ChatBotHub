using ChatBotHub.Domain.ChatBots;

namespace ChatBotHub.Application.ChatBots;

public interface IChatBotQueryService {
    Task<ChatBot?> GetByIdAsync(Guid id);
    Task<ChatBot> RequireAsync(Guid id);
    Task<List<ChatBot>> GetAllAsync();
    Task<bool> ExistsAsync(Guid id);
}
