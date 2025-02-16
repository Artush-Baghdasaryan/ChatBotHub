using ChatBotHub.WebApi.Infrastructure.Context;
using ChatBotHub.WebApi.Infrastructure.Domain;

namespace ChatBotHub.WebApi.Infrastructure.Repositories;

public class ChatBotRepository : BaseRepository<ChatBot> {
    public ChatBotRepository(DataContext context) : base(context, "chatBots") {
    }
}