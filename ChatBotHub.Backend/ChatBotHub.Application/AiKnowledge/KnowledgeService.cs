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
    private readonly IFileQueryService _fileQueryService;

    public KnowledgeService(
        IAiKnowledgeClient client,
        IChatBotQueryService chatBotQueryService,
        IAttachmentQueryService attachmentQueryService,
        IFileQueryService fileQueryService
    ) {
        _client = client;
        _chatBotQueryService = chatBotQueryService;
        _attachmentQueryService = attachmentQueryService;
        _fileQueryService = fileQueryService;
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

        await _client.IndexAttachmentsAsync(botId, indexRequests);
    }

    private async Task<List<Attachment>> GetAttachmentsToIndex(Guid botId) {
        var bot = await _chatBotQueryService.RequireAsync(botId);
        var attachments = await _attachmentQueryService.GetByIdsAsync(bot.AttachmentsIds);
        return attachments.Where(a => !a.Indexed).ToList();
    } 

    public async Task RemoveKnowledgeAsync(Guid botId, Guid attachmentId) {
        var bot = await _chatBotQueryService.RequireAsync(botId);
        if (bot.AttachmentsIds.Contains(attachmentId)) {
            await _client.RemoveAttachmentIndexAsync(botId, attachmentId);
        }
    }
}
