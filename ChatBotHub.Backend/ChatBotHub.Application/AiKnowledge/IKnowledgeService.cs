using ChatBotHub.Application.AiKnowledge.Models.Requests;

namespace ChatBotHub.Application.AiKnowledge;

public interface IKnowledgeService {
    Task IndexKnowledgeAsync(Guid botId);
    Task<string> QueryKnowledgeAsync(Guid botId, QueryKnowledgeRequest request);
    Task RemoveKnowledgeAsync(Guid botId, Guid attachmentId);
}
