using ChatBotHub.Application.AiKnowledge.Requests;
using ChatBotHub.Application.Sessions;
using ChatBotHub.Domain.Session;

namespace ChatBotHub.Application.AiKnowledge;

public class KnowledgeQueryService : IKnowledgeQueryService {
    private const int MaxContextMessages = 10;

    private readonly IAiKnowledgeClient _client;
    private readonly ISessionQueryService _sessionQueryService;
    private readonly ISessionCommandService _sessionCommandService;

    public KnowledgeQueryService(
        IAiKnowledgeClient client,
        ISessionQueryService sessionQueryService,
        ISessionCommandService sessionCommandService
    ) {
        _client = client;
        _sessionQueryService = sessionQueryService;
        _sessionCommandService = sessionCommandService;
    }

    public async Task<string> QueryAsync(Guid botId, string query, Guid? sessionId = null) {
        List<Message>? context = null;
        if (sessionId is not null) {
            var sesion = await _sessionQueryService.RequireByIdAsync(sessionId.Value);
            context = sesion.Messages.Take(MaxContextMessages).ToList();
        }

        var request = new QueryRequest {
            Query = query,
            Context = context
        };

        return await _client.QueryAsync(botId, request);
    }
}
