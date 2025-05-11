using System.Xml.Schema;
using ChatBotHub.Application.Sessions.Exceptions;
using ChatBotHub.Application.Sessions.Requests;
using ChatBotHub.Domain.Session;

namespace ChatBotHub.Application.Sessions;

public class SessionCommandService : ISessionCommandService {
    private readonly ISessionRepository _sessionRepository;
    private readonly ISessionQueryService _sessionQueryService;

    public SessionCommandService(
        ISessionRepository sessionRepository,
        ISessionQueryService sessionQueryService
    ) {
        _sessionRepository = sessionRepository;
        _sessionQueryService = sessionQueryService;
    }

    public async Task<Session> AddMessageAsync(Guid id, AddMessageRequest request) {
        var session = await _sessionQueryService.RequireByIdAsync(id);
        var message = Message.Create(request.Role, request.Content);
        session.AddMessage(message);

        await _sessionRepository.UpdateAsync(session);

        return session;
    }

    public async Task<Session> CreateSessionAsync(Guid botId) {
        var session = new Session(botId);
        await _sessionRepository.InsertAsync(session);

        return session;
    }

    public async Task DeleteAsync(Guid id) {
        if (!await _sessionRepository.ExistsAsync(id)) {
            throw new SessionNotFoundException(id);
        }

        await _sessionRepository.DeleteAsync(id);
    }
}
