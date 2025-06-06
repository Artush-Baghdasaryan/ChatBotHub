using ChatBotHub.Application.Sessions.Requests;
using ChatBotHub.Domain.Session;

namespace ChatBotHub.Application.Sessions;

public interface ISessionCommandService {
    Task<Session> CreateSessionAsync(Guid botId);
    Task<Session> AddMessagesAsync(Guid id, List<AddMessageRequest> request);
    Task DeleteAsync(Guid id);
}


