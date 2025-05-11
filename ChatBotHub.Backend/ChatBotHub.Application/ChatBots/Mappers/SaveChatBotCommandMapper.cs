using ChatBotHub.Application.ChatBots.Requests;
using ChatBotHub.Domain.ChatBots.Commands;

namespace ChatBotHub.Application.ChatBots.Mappers;

public static class SaveChatBotCommandMapper {
    public static SaveChatBotCommand Map(SaveChatBotRequest request) {
        return new SaveChatBotCommand {
            Name = request.Name,
            Description = request.Description
        };
    }
}
