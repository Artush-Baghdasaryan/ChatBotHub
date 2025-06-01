using ChatBotHub.Application.AiKnowledge.Mappers;
using ChatBotHub.Application.AiKnowledge.Models.Requests;
using ChatBotHub.Application.AiKnowledge.Requests;
using ChatBotHub.Application.Attachments;
using ChatBotHub.Application.ChatBots;
using ChatBotHub.Application.Files;
using ChatBotHub.Domain.Attachments;

namespace ChatBotHub.Application.AiKnowledge;

public class KnowledgeService : IKnowledgeService {
    private readonly IAiKnowledgeClient _client;
    private readonly IChatBotQueryService _chatBotQueryService;
    private readonly IAttachmentQueryService _attachmentQueryService;
    private readonly IAttachmentCommandService _attachmentCommandService;
    private readonly IFileQueryService _fileQueryService;

    public KnowledgeService(
        IAiKnowledgeClient client,
        IChatBotQueryService chatBotQueryService,
        IAttachmentQueryService attachmentQueryService,
        IFileQueryService fileQueryService,
        IAttachmentCommandService attachmentCommandService
    ) {
        _client = client;
        _chatBotQueryService = chatBotQueryService;
        _attachmentQueryService = attachmentQueryService;
        _fileQueryService = fileQueryService;
        _attachmentCommandService = attachmentCommandService;
    }

    public async Task IndexKnowledgeAsync(Guid botId) {
        var attachments = await GetAttachmentsToIndex(botId);
        if (!attachments.Any()) {
            return;
        }

        var indexRequests = new List<IndexKnowledgeRequest>();
        foreach (var attachment in attachments) {
            var file = await _fileQueryService.RequireByIdAsync(attachment.FileId);
            indexRequests.Add(new IndexKnowledgeRequest {
                AttachmentId = attachment.Id,
                Description = attachment.Description,
                FileName = file.FileName,
                FileData = file.FileData,
                FileType = file.FileType
            });
        }

        await _client.IndexAttachmentsAsync(indexRequests);
        await _attachmentCommandService.MakeAttachmentsIndexed(attachments);
    }

    private async Task<List<Attachment>> GetAttachmentsToIndex(Guid botId) {
        var bot = await _chatBotQueryService.RequireAsync(botId);
        var attachments = await _attachmentQueryService.GetByIdsAsync(bot.AttachmentsIds);
        return attachments.Where(a => !a.Indexed).ToList();
    }
    
    public async Task<string> QueryKnowledgeAsync(
        Guid botId,
        QueryKnowledgeRequest request,
        Guid? sessionId = null
    ) {
        var bot = await _chatBotQueryService.RequireAsync(botId);
        var attachments = await _attachmentQueryService.GetByIdsAsync(bot.AttachmentsIds);
        var externalRequest = new ExternalQueryKnowledgeRequest {
            Query = request.Query,
            ChatBot = ExternalChatBotModelMapper.Map(bot, attachments),
            SessionId = sessionId
        };
        
        return await _client.QueryAsync(externalRequest);
    }

    public Task RemoveKnowledgeAsync(Guid botId, Guid attachmentId) {
         return _client.RemoveAttachmentIndexAsync(attachmentId);
    }
}
