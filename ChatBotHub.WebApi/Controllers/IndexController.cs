using Application.Models.Requests;
using ChatBotHub.WebApi.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace ChatBotHub.WebApi.Controllers;

[ApiController]
[Route("api/[controller]/{chatBotId}")]
public class IndexController : ControllerBase {
    private readonly IIndexService _indexService;

    public IndexController(IIndexService indexService) {
        _indexService = indexService;
    }

    [HttpPost("index")]
    public async Task Index([FromRoute] Guid chatBotId, [FromBody] List<AttachmentCreateModel> attachments) {
        await _indexService.IndexAsync(chatBotId, attachments);
    }

    [HttpGet("query")]
    public async Task<string> Query([FromRoute] Guid chatBotId, [FromQuery] string query) {
        return await _indexService.QueryAsync(chatBotId, query);
    }
}
