using ChatBotHub.Application.AiKnowledge.Models;
using ChatBotHub.Domain.Attachments;
using ChatBotHub.Domain.ChatBots;

namespace ChatBotHub.Application.AiKnowledge.Mappers;

public static class ExternalChatBotModelMapper {
    public static ExternalChatBotModel Map(
        ChatBot chatBot,
        List<Attachment> attachments
    ) {
        return new ExternalChatBotModel {
            Id = chatBot.Id,
            Name = chatBot.Name,
            Description = chatBot.Description,
            Attachments = attachments.Select(ExternalAttachmentModelMapper.Map).ToList()
        };
    }
}
