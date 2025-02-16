using Application.Models.Requests;
using ChatBotHub.WebApi.Application.Mapper;
using ChatBotHub.WebApi.Application.Models.Dto;
using ChatBotHub.WebApi.Infrastructure.Repositories;

namespace ChatBotHub.WebApi.Application.Services.Implementations;

public class IndexService : IIndexService {
    private readonly AttachmentRepository _attachmentRepository;
    private readonly FileModelRepository _fileModelRepository;
    private readonly IChatBotService     _chatBotService;
    private readonly IRagService _ragService;

    public IndexService(
        AttachmentRepository attachmentRepository,
        FileModelRepository fileModelRepository,
        IChatBotService chatBotService,
        IRagService ragService) {
        _attachmentRepository = attachmentRepository;
        _fileModelRepository = fileModelRepository;
        _chatBotService = chatBotService;
        _ragService = ragService;
    }

    public async Task IndexAsync(Guid chatBotId, List<AttachmentCreateModel> attachments) {
        if (!await _chatBotService.ExistsAsync(chatBotId)) {
            throw new Exception("Chat bot is not found");
        }

        var attachmentModels = new List<AttachmentModel>();

        foreach (var attachment in attachments) {
            var attachmentEntity = AttachmentMapper.Map(attachment);
            await _attachmentRepository.CreateAsync(attachmentEntity);

            var file = await _fileModelRepository.GetByIdAsync(attachment.FileId);
            if (file is null) {
                throw new ArgumentException($"Invalid file Id is provided {attachment.FileId}");
            }

            var fileModel = FileModelMapper.Map(file);
            var attachmentModel = AttachmentMapper.Map(attachmentEntity, fileModel);
            attachmentModels.Add(attachmentModel);
        }

        await _ragService.IndexAsync(chatBotId, attachmentModels);
    }

    public async Task<string> QueryAsync(Guid chatBotId, string query) {
        if (!await _chatBotService.ExistsAsync(chatBotId)) {
            throw new Exception("ChatBot not found");
        }

        return await _ragService.QueryAsync(chatBotId, query);
    }
}