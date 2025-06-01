namespace ChatBotHub.Application.AiKnowledge;

public interface IKnowledgeQueryService {
    Task<string> QueryAsync(Guid botId, string query, Guid sessionId);
}
