using ChatBotHub.Application.Attachments;
using ChatBotHub.WebApi.Mappers.Attachments;
using ChatBotHub.WebApi.Models.Attachments;
using Microsoft.AspNetCore.Mvc;

namespace ChatBotHub.WebApi.Controllers;

[Route("api/attachments")]
public class AttachmentController : BaseController {
    private readonly IAttachmentQueryService _attachmentQueryService;
    private readonly IAttachmentCommandService _attachmentCommandService;

    public AttachmentController(
        IAttachmentQueryService attachmentQueryService,
        IAttachmentCommandService attachmentCommandService
    ) {
        _attachmentQueryService = attachmentQueryService;
        _attachmentCommandService = attachmentCommandService;
    }

    [HttpGet("{id:guid}")]
    public async Task<AttachmentModel?> GetById(Guid id) {
        var attachment = await _attachmentQueryService.GetByIdAsync(id);
        return AttachmentMapper.Map(attachment);
    }
}
