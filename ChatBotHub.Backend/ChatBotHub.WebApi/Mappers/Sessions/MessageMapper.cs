using ChatBotHub.Domain.Session;
using ChatBotHub.WebApi.Models.Sessions;

namespace ChatBotHub.WebApi.Mappers.Sessions;

public static class MessageMapper {
    public static MessageModel Map(Message message) {
        return new MessageModel {
            Role = (MessageRoleTypeModel)message.Role,
            Content = message.Content,
            DateTime = message.DateTime,
        };
    }
}
