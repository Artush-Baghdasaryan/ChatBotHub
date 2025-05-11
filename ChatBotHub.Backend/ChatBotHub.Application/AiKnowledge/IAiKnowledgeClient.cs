using ChatBotHub.Application.AiKnowledge.Requests;

namespace ChatBotHub.Application.AiKnowledge;

public interface IAiKnowledgeClient {
    Task IndexAttachmentsAsync(Guid botId, List<IndexKnowledgeRequest> requests);
    Task RemoveAttachmentIndexAsync(Guid botId, Guid attachmentId);
    Task<string> QueryAsync(Guid botId, QueryRequest request);
}
