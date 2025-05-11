using ChatBotHub.Domain.Session;
using ChatBotHub.Infrastructure.Common;
using ChatBotHub.Infrastructure.MongoDb;

namespace ChatBotHub.Infrastructure.Repositories;

public class SessionRepository : DataRepository<Session>, ISessionRepository {
    public SessionRepository(MongoDbDataContext dbContext) : base(dbContext, "sessions") {
    }

    public Task<List<Session>> GetAllByBotIdAsync(Guid botId) {
        return FindAsync(session => session.BotId == botId);
    }
}
