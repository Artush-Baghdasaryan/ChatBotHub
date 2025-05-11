namespace ChatBotHub.Application.AiKnowledge;

public interface IKnowledgeService {
    Task IndexKnowledgeAsync(Guid botId);
    Task RemoveKnowledgeAsync(Guid botId, Guid attachmentId);
}
