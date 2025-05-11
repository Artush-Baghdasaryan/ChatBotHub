using ChatBotHub.Domain.Session;

namespace ChatBotHub.Application.Sessions;

public interface ISessionQueryService {
    Task<Session?> GetByIdAsync(Guid id);
    Task<Session> RequireByIdAsync(Guid id);
    Task<List<Session>> GetAllByBotIdAsync(Guid botId);
}

