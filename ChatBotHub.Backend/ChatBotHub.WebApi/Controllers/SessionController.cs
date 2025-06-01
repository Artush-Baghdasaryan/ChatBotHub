using ChatBotHub.Application.Sessions;
using ChatBotHub.WebApi.Mappers.Sessions;
using ChatBotHub.WebApi.Models.Sessions;
using Microsoft.AspNetCore.Mvc;

namespace ChatBotHub.WebApi.Controllers;

[Route("api/sessions")]
public class SessionController : BaseController {
    private readonly ISessionQueryService _sessionQueryService;
    private readonly ISessionCommandService _sessionCommandService;

    public SessionController(
        ISessionQueryService sessionQueryService,
        ISessionCommandService sessionCommandService
    ) {
        _sessionQueryService = sessionQueryService;
        _sessionCommandService = sessionCommandService;
    }

    [HttpGet("{id:guid}")]
    public async Task<SessionModel?> Get(Guid id) {
        var session = await _sessionQueryService.GetByIdAsync(id);
        return SessionMapper.Map(session);
    }

    [HttpPost]
    public async Task<SessionModel> Create([FromQuery] Guid botId) {
        var session = await _sessionCommandService.CreateSessionAsync(botId);
        return SessionMapper.Map(session);
    }

    [HttpDelete("{id:guid}")]
    public Task Delete(Guid id) {
        return _sessionCommandService.DeleteAsync(id);
    }
}
