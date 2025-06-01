using ChatBotHub.Application.AiKnowledge.Requests;

namespace ChatBotHub.Application.AiKnowledge;

public interface IKnowledgeService {
    Task IndexKnowledgeAsync(Guid botId);
    Task<string> QueryKnowledgeAsync(Guid botId, QueryKnowledgeRequest request, Guid? sessionId = null);
    Task RemoveKnowledgeAsync(Guid botId, Guid attachmentId);
}
