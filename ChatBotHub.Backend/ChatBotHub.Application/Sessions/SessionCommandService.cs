using System.Xml.Schema;
using ChatBotHub.Application.ChatBots;
using ChatBotHub.Application.ChatBots.Exceptions;
using ChatBotHub.Application.Sessions.Exceptions;
using ChatBotHub.Application.Sessions.Requests;
using ChatBotHub.Domain.Session;

namespace ChatBotHub.Application.Sessions;

public class SessionCommandService : ISessionCommandService {
    private readonly ISessionRepository _sessionRepository;
    private readonly ISessionQueryService _sessionQueryService;
    private readonly IChatBotQueryService _chatBotQueryService;

    public SessionCommandService(
        ISessionRepository sessionRepository,
        ISessionQueryService sessionQueryService,
        IChatBotQueryService chatBotQueryService
    ) {
        _sessionRepository = sessionRepository;
        _sessionQueryService = sessionQueryService;
        _chatBotQueryService = chatBotQueryService;
    }

    public async Task<Session> AddMessagesAsync(Guid id, List<AddMessageRequest> requests) {
        var session = await _sessionQueryService.RequireByIdAsync(id);

        foreach (var request in requests) {
            var message = Message.Create(request.Role, request.Content);
            session.AddMessage(message);
        }
        
        await _sessionRepository.UpdateAsync(session);
        return session;
    }

    public async Task<Session> CreateSessionAsync(Guid botId) {
        if (!await _chatBotQueryService.ExistsAsync(botId)) {
            throw new ChatBotIsNotFoundException(botId);
        }

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
