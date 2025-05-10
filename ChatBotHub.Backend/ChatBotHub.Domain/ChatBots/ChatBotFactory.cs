using ChatBotHub.Domain.ChatBots.Commands;

namespace ChatBotHub.Domain.ChatBots;

public static class ChatBotFactory {
    public static ChatBot Create(Guid accountId, SaveChatBotCommand command) {
        var bot = new ChatBot(accountId);
        
        bot.SetName(command.Name);
        bot.SetDescription(command.Description);

        return bot;
    }
}
