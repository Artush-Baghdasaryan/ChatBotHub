﻿using ChatBotHub.Application.Attachments.Requests;
using ChatBotHub.Application.ChatBots.Requests;
using ChatBotHub.Domain.ChatBots;

namespace ChatBotHub.Application.ChatBots;

public interface IChatBotCommandService {
    Task<ChatBot> CreateAsync(Guid accountId, SaveChatBotRequest request);
    Task<ChatBot> UpdateAsync(Guid id, SaveChatBotRequest request);
    Task<ChatBot> AddAttachmentAsync(Guid botId, SaveAttachmentRequest request);
    Task<ChatBot> RemoveAttachmentAsync(Guid botId, Guid attachmentId);
    Task DeleteAsync(Guid id);
}
