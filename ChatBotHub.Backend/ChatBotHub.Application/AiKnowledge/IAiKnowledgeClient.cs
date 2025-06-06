﻿using ChatBotHub.Application.AiKnowledge.Models.Requests;

namespace ChatBotHub.Application.AiKnowledge;

public interface IAiKnowledgeClient {
    Task IndexAttachmentsAsync(List<IndexKnowledgeRequest> requests);
    Task RemoveAttachmentIndexAsync(Guid attachmentId);
    Task<string> QueryAsync(ExternalQueryKnowledgeRequest request);
}
