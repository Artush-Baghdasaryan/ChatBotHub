using ChatBotHub.Application.ChatBots;
using ChatBotHub.Application.Attachments;
using ChatBotHub.WebApi.Mappers.Attachments;
using ChatBotHub.WebApi.Mappers.ChatBots;
using ChatBotHub.WebApi.Models.Attachments;
using ChatBotHub.WebApi.Models.ChatBots;
using Microsoft.AspNetCore.Components.Forms.Mapping;
using Microsoft.AspNetCore.Mvc;

namespace ChatBotHub.WebApi.Controllers;

[Route("api/chatbots")]
public class ChatBotController : BaseController {
    private readonly IChatBotQueryService _chatBotQueryService;
    private readonly IChatBotCommandService _chatBotCommandService;
    private readonly IAttachmentQueryService _attachmentQueryService;

    public ChatBotController(
        IChatBotQueryService chatBotQueryService,
        IChatBotCommandService chatBotCommandService,
        IAttachmentQueryService attachmentQueryService
    ) {
        _chatBotQueryService = chatBotQueryService;
        _chatBotCommandService = chatBotCommandService;
        _attachmentQueryService = attachmentQueryService;
    }

    [HttpGet("{id:guid}")]
    public async Task<ExtendedChatBotModel?> GetById(Guid id) {
        var bot = await _chatBotQueryService.GetByIdAsync(id);
        var attachments = await _attachmentQueryService.GetByIdsAsync(bot?.AttachmentsIds ?? []);

        return ExtendedChatBotMapper.Map(bot, attachments);
    }

    [HttpGet]
    public async Task<List<ChatBotModel>> GetAll() {
        var chatBots = await _chatBotQueryService.GetAllAsync();
        return chatBots.Select(e => ChatBotMapper.Map(e)).ToList();
    }

    [HttpPut("{id:guid}")]
    public async Task<ChatBotModel?> Update(Guid id, [FromBody] SaveChatBotRequestModel requestModel) {
        var request = SaveChatBotRequestMapper.Map(requestModel);
        var bot = await _chatBotCommandService.UpdateAsync(id, request);

        return ChatBotMapper.Map(bot);
    }

    [HttpPost]
    public async Task<ChatBotModel?> Create([FromBody] SaveChatBotRequestModel requestModel) {
        var request = SaveChatBotRequestMapper.Map(requestModel);
        var bot = await _chatBotCommandService.CreateAsync(GetAccountId(), request);

        return ChatBotMapper.Map(bot);
    }

    [HttpPost("{id:guid}/attachments")]
    public Task AddAttachment(Guid id, [FromBody] SaveAttachmentRequestModel requestModel) {
        var request = SaveAttachmentRequestMapper.Map(requestModel);
        return _chatBotCommandService.AddAttachmentAsync(id, request);
    }

    [HttpDelete("{id:guid}/attachments")]
    public Task RemoveAttachment(Guid id, [FromQuery] Guid attachmentId) {
        return _chatBotCommandService.RemoveAttachmentAsync(id, attachmentId);
    }

    [HttpDelete("{id:guid}")]
    public Task DeleteAsync(Guid id) {
        return _chatBotCommandService.DeleteAsync(id);
    }
}
