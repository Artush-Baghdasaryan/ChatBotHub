using ChatBotHub.Domain.ChatBots;
using ChatBotHub.Infrastructure.Common;
using ChatBotHub.Infrastructure.MongoDb;

namespace ChatBotHub.Infrastructure.Repositories;

public class ChatBotRepository : DataRepository<ChatBot>, IChatBotRepository {
    public ChatBotRepository(MongoDbDataContext context) : base(context, "chatBots") {
    }
}
