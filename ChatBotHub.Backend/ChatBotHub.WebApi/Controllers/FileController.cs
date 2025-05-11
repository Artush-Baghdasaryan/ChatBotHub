using ChatBotHub.Application.Files;
using ChatBotHub.WebApi.Mappers.Files;
using ChatBotHub.WebApi.Models.Files;
using Microsoft.AspNetCore.Mvc;

namespace ChatBotHub.WebApi.Controllers;

[Route("api/files")]
public class FileController : BaseController {
    private readonly IFileQueryService _fileQueryService;
    private readonly IFileCommandService _fileCommandService;

    public FileController(
        IFileQueryService fileQueryService,
        IFileCommandService fileCommandService
    ) {
        _fileQueryService = fileQueryService;
        _fileCommandService = fileCommandService;
    }
    
    [HttpGet("{id}")]
    public async Task<FileModel?> GetById(Guid id) {
        var file = await _fileQueryService.GetByIdAsync(id);
        return FileMapper.Map(file);
    }

    [HttpPost]
    public async Task<FileModel> Upload([FromForm] IFormFile formFile) {
        var file = await _fileCommandService.CreateAsync(formFile);
        return FileMapper.Map(file);
    }

    [HttpDelete("{id}")]
    public Task Delete(Guid id) {
        return _fileCommandService.DeleteAsync(id);
    }
    
}

