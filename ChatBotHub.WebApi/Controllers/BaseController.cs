using System.Security.Claims;
using ChatBotHub.WebApi.Application.Services;
using ChatBotHub.WebApi.Infrastructure.Domain;
using Microsoft.AspNetCore.Mvc;

namespace ChatBotHub.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BaseController<T> : ControllerBase where T : BaseModel {

    private readonly IBaseService<T> _baseService;

    public BaseController(IBaseService<T> baseService) {
        _baseService = baseService;
    }

    [HttpGet("{entityId}")]
    public async Task<IActionResult> Get(Guid entityId) {
        var entity = await _baseService.GetByIdAsync(entityId);
        return Ok(entity);
    }

    [HttpGet("get-all")]
    public async Task<IActionResult> GetAll() {
        var entities = await _baseService.GetAllAsync();
        return Ok(entities);
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] T entity) {
        var entityId = await _baseService.CreateAsync(entity);
        return Ok(entityId);
    }

    [HttpPut("update")]
    public async Task<IActionResult> Update([FromBody] T entity) {
        await _baseService.UpdateAsync(entity);
        return Ok();
    }

    [HttpDelete("{entityId}")]
    public async Task<IActionResult> Delete(Guid entityId) {
        await _baseService.DeleteAsync(entityId);
        return Ok();
    }

    [HttpDelete("delete-all")]
    public async Task<IActionResult> DeleteAll() {
        await _baseService.DeleteAllAsync();
        return Ok();
    }

    protected Guid GetUserId() {
        var claimValue = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(claimValue) || !Guid.TryParse(claimValue, out var id))
        {
            throw new UnauthorizedAccessException();
        }
        return id;
    }
}


