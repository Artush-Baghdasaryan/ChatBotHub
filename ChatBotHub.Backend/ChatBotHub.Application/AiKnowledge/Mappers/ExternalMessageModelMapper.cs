using ChatBotHub.Application.AiKnowledge.Models;
using ChatBotHub.Domain.Session;

namespace ChatBotHub.Application.AiKnowledge.Mappers;

public static class ExternalMessageModelMapper {
    public static ExternalMessageModel Map(Message message) {
        return new ExternalMessageModel {
            Role = message.Role,
            Content = message.Content
        };
    }
}
