using ChatBotHub.WebApi.Application.Services;
using ChatBotHub.WebApi.Dto;
using ChatBotHub.WebApi.Infrastructure.Domain;
using Microsoft.AspNetCore.Mvc;

namespace ChatBotHub.WebApi.Controllers;

public class ChatBotController : BaseController<ChatBot> {
    private readonly IChatBotService _chatBotService;

    public ChatBotController(IChatBotService chatBotService) : base(chatBotService) {
        _chatBotService = chatBotService;
    }

    [HttpPost("create-bot")]
    public Task<Guid> CreateBot([FromForm] string name, [FromForm] List<FileUploadDto> files) {
        return _chatBotService.CreateBotAsync(GetUserId(), name, files);    
    }
    
}
