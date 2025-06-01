using ChatBotHub.Application.AiKnowledge;
using ChatBotHub.WebApi.Mappers.Knowledge;
using ChatBotHub.WebApi.Models.Knowledge;
using Microsoft.AspNetCore.Mvc;

namespace ChatBotHub.WebApi.Controllers;

[Route("api/knowledge")]
public class KnowledgeController : BaseController {
    private readonly IKnowledgeService _knowledgeService;

    public KnowledgeController(IKnowledgeService knowledgeService) {
        _knowledgeService = knowledgeService;
    }

    [HttpPost("{botId:guid}/index")]
    public Task Index(Guid botId) {
        return _knowledgeService.IndexKnowledgeAsync(botId);
    }

    [HttpPost("{botId:guid}/query")]
    public Task<string> Query(Guid botId, [FromBody] QueryKnowledgeRequestModel requestModel) {
        var request = KnowledgeMapper.Map(requestModel);
        return _knowledgeService.QueryKnowledgeAsync(botId, request);
    }
}
