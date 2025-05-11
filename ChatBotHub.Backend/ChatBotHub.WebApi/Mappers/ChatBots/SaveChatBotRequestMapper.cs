using ChatBotHub.Application.ChatBots.Requests;
using ChatBotHub.WebApi.Models.ChatBots;

namespace ChatBotHub.WebApi.Mappers.ChatBots;

public static class SaveChatBotRequestMapper {
    public static SaveChatBotRequest Map(SaveChatBotRequestModel model) {
        return new SaveChatBotRequest {
            Name = model.Name,
            Description = model.Description,
        };
    }
}
