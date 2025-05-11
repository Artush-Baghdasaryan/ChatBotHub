using ChatBotHub.Application.AiKnowledge;
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
}
