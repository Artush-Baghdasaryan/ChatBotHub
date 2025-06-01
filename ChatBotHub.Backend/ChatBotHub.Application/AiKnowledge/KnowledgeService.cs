using ChatBotHub.Application.AiKnowledge.Mappers;
using ChatBotHub.Application.AiKnowledge.Models;
using ChatBotHub.Application.AiKnowledge.Models.Requests;
using ChatBotHub.Application.Attachments;
using ChatBotHub.Application.ChatBots;
using ChatBotHub.Application.Files;
using ChatBotHub.Application.Sessions;
using ChatBotHub.Application.Sessions.Requests;
using ChatBotHub.Domain.Attachments;
using ChatBotHub.Domain.Session;

namespace ChatBotHub.Application.AiKnowledge;

public class KnowledgeService : IKnowledgeService {
    private readonly IAiKnowledgeClient _client;
    private readonly IChatBotQueryService _chatBotQueryService;
    private readonly IAttachmentQueryService _attachmentQueryService;
    private readonly IAttachmentCommandService _attachmentCommandService;
    private readonly IFileQueryService _fileQueryService;
    
    private readonly ISessionQueryService _sessionQueryService;
    private readonly ISessionCommandService _sessionCommandService;

    public KnowledgeService(
        IAiKnowledgeClient client,
        IChatBotQueryService chatBotQueryService,
        IAttachmentQueryService attachmentQueryService,
        IFileQueryService fileQueryService,
        IAttachmentCommandService attachmentCommandService,
        ISessionCommandService sessionCommandService,
        ISessionQueryService sessionQueryService
    ) {
        _client = client;
        _chatBotQueryService = chatBotQueryService;
        _attachmentQueryService = attachmentQueryService;
        _fileQueryService = fileQueryService;
        _attachmentCommandService = attachmentCommandService;
        _sessionCommandService = sessionCommandService;
        _sessionQueryService = sessionQueryService;
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
        QueryKnowledgeRequest request
    ) {
        var bot = await _chatBotQueryService.RequireAsync(botId);
        var attachments = await _attachmentQueryService.GetByIdsAsync(bot.AttachmentsIds);

        var session = request.SessionId.HasValue
            ? await _sessionQueryService.GetByIdAsync(request.SessionId.Value)
            : null;

        var externalRequest = new ExternalQueryKnowledgeRequest {
            Query = request.Query,
            ChatBot = ExternalChatBotModelMapper.Map(bot, attachments),
            ChatHistory = (session?.Messages ?? []).Select(ExternalMessageModelMapper.Map).ToList()
        };
        
        var response = await _client.QueryAsync(externalRequest);
        if (session is not null) {
            var messages = new List<AddMessageRequest> {
                new (request.Query, MessageRoleType.Bot),
                new (response, MessageRoleType.Bot)
            };

            await _sessionCommandService.AddMessagesAsync(session.Id, messages);
        }
        
        return response;
    }

    public Task RemoveKnowledgeAsync(Guid botId, Guid attachmentId) {
         return _client.RemoveAttachmentIndexAsync(attachmentId);
    }
}
