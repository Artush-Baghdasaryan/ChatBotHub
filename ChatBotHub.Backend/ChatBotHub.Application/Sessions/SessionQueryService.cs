using ChatBotHub.Application.Sessions.Exceptions;
using ChatBotHub.Domain.Session;

namespace ChatBotHub.Application.Sessions;

public class SessionQueryService : ISessionQueryService {
    private readonly ISessionRepository _sessionRepository;

    public SessionQueryService(ISessionRepository sessionRepository) {
        _sessionRepository = sessionRepository;
    }

    public Task<Session?> GetByIdAsync(Guid id) {
        return _sessionRepository.GetByIdAsync(id);
    }

    public async Task<Session> RequireByIdAsync(Guid id) {
        return await GetByIdAsync(id) ?? throw new SessionNotFoundException(id);
    }

    public Task<List<Session>> GetAllByBotIdAsync(Guid botId) {
        return _sessionRepository.GetAllByBotIdAsync(botId);
    }
}
