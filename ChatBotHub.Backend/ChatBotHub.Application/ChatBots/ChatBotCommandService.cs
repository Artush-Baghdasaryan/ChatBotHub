using ChatBotHub.Application.Attachments;
using ChatBotHub.Application.Attachments.Requests;
using ChatBotHub.Application.ChatBots.Exceptions;
using ChatBotHub.Application.ChatBots.Mappers;
using ChatBotHub.Application.ChatBots.Requests;
using ChatBotHub.Domain.ChatBots;

namespace ChatBotHub.Application.ChatBots;

public class ChatBotCommandService : IChatBotCommandService {
    private readonly IChatBotRepository _chatBotRepository;
    private readonly IChatBotQueryService _chatBotQueryService;
    private readonly IAttachmentCommandService _attachmentCommandService;

    public ChatBotCommandService(
        IChatBotRepository chatBotRepository,
        IChatBotQueryService chatBotQueryService,
        IAttachmentCommandService attachmentCommandService
    ) {
        _chatBotRepository = chatBotRepository;
        _chatBotQueryService = chatBotQueryService;
        _attachmentCommandService = attachmentCommandService;
    }

    public async Task<ChatBot> CreateAsync(Guid accountId, SaveChatBotRequest request) {
        var command = SaveChatBotCommandMapper.Map(request);
        var bot = ChatBotFactory.Create(accountId, command);
        await _chatBotRepository.InsertAsync(bot);

        return bot;
    }

    public async Task<ChatBot> UpdateAsync(Guid id, Guid accountId, SaveChatBotRequest request) {
        var bot = await _chatBotQueryService.RequireAsync(id);
        
        bot.SetName(request.Name);
        bot.SetDescription(request.Description);

        await _chatBotRepository.UpdateAsync(bot);
        return bot;
    }

    public async Task<ChatBot> AddAttachmentAsync(Guid botId, SaveAttachmentRequest request) {
        var bot = await _chatBotQueryService.RequireAsync(botId);
        var attachment = await _attachmentCommandService.CreateAsync(botId, request);
        
        bot.AddAttachment(attachment.Id!.Value);
        await _chatBotRepository.UpdateAsync(bot);

        return bot;
    }

    public async Task<ChatBot> RemoveAttachmentAsync(Guid botId, Guid attachmentId) {
        var bot = await _chatBotQueryService.RequireAsync(botId);
        await _attachmentCommandService.DeleteAsync(attachmentId);

        bot.RemoveAttachment(attachmentId);
        await _chatBotRepository.UpdateAsync(bot);
        
        return bot;
    }

    public async Task DeleteAsync(Guid id) {
        if (!await _chatBotRepository.ExistsAsync(id)) {
            throw new ChatBotIsNotFoundException(id);
        }

        await _chatBotRepository.DeleteAsync(id);
    }
}
