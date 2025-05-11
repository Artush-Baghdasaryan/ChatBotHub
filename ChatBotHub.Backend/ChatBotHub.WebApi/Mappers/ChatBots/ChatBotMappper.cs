using System.Diagnostics.CodeAnalysis;
using ChatBotHub.Domain.Attachments;
using ChatBotHub.Domain.ChatBots;
using ChatBotHub.WebApi.Mappers.Attachments;
using ChatBotHub.WebApi.Mappers.Common;
using ChatBotHub.WebApi.Models.ChatBots;

namespace ChatBotHub.WebApi.Mappers.ChatBots;

public static class ChatBotMapper {
    [return: NotNullIfNotNull(nameof(bot))]
    public static ChatBotModel? Map(ChatBot? bot) {
        if (bot is null) {
            return null;
        }

        return new ChatBotModel {
            Id = bot.Id,
            Name = bot.Name,
            Description = bot.Description,
            AttachmentsIds = bot.AttachmentsIds,
            Audit = AuditMapper.Map(bot.Audit),
        };
    }
}
