using ChatBotHub.Domain.Common.Repositories;

namespace ChatBotHub.Domain.Session;

public interface ISessionRepository : IRepository<Session> {
    Task<List<Session>> GetAllByBotIdAsync(Guid botId);        
}
