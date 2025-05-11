using System.Diagnostics.CodeAnalysis;
using ChatBotHub.Domain.Attachments;
using ChatBotHub.Domain.ChatBots;
using ChatBotHub.WebApi.Mappers.Attachments;
using ChatBotHub.WebApi.Mappers.Common;
using ChatBotHub.WebApi.Models.ChatBots;

namespace ChatBotHub.WebApi.Mappers.ChatBots;

public static class ExtendedChatBotMapper {
    [return: NotNullIfNotNull(nameof(bot))]
    public static ExtendedChatBotModel? Map(ChatBot? bot, List<Attachment>? attachments = null) {
        if (bot is null) {
            return null;
        }

        return new ExtendedChatBotModel {
            Id = bot.Id,
            Name = bot.Name,
            Description = bot.Description,
            Attachments = [.. (attachments ?? []).Select(AttachmentMapper.Map)],
            Audit = AuditMapper.Map(bot.Audit),
        };
    }
}

