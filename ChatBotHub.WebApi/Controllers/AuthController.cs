using Application.Models.Requests;
using Application.Models.Responses;
using ChatBotHub.WebApi.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace ChatBotHub.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public Task<LoginResultModel> Login([FromBody] LoginRequestModel request) {
        return _authService.LoginAsync(request);
    }

    [HttpPost("register")]
    public Task Register([FromBody] RegisterRequestModel request) {
        return _authService.RegisterAsync(request);
    }
}
