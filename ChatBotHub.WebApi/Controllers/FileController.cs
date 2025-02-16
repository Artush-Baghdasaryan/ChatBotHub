using ChatBotHub.WebApi.Application.Services;
using ChatBotHub.WebApi.Infrastructure.Domain;
using Microsoft.AspNetCore.Mvc;

namespace ChatBotHub.WebApi.Controllers;

public class FileController : BaseController<FileModel> {
    private readonly IFileService _fileService;

    public FileController(IFileService fileService) : base(fileService) {
        _fileService = fileService;
    }

    [HttpPost("upload")]
    public async Task<ActionResult> Upload(IFormFile file) {
        try {
            var fileId = await _fileService.UploadFileAsync(file);
            return Ok(fileId);
        } catch (Exception e) {
            return BadRequest(e.Message);
        }
    }
}

