using ChatBotHub.Application.Accounts;
using ChatBotHub.WebApi.Mappers.Accounts;
using ChatBotHub.WebApi.Models.Accounts;
using Microsoft.AspNetCore.Mvc;

namespace ChatBotHub.WebApi.Controllers;

[Route("api/accounts")]
public class AccountController : BaseController {
    private readonly IAccountQueryService _accountQueryService;
    private readonly IAccountAuthenticationService _accountAuthenticationService;

    public AccountController(
        IAccountQueryService accountQueryService,
        IAccountAuthenticationService accountAuthenticationService
    ) {
        _accountQueryService = accountQueryService;
        _accountAuthenticationService = accountAuthenticationService;
    }

    [HttpGet("me")]
    public async Task<AccountModel> GetMe() {
        var account = await _accountQueryService.RequireByIdAsync(GetAccountId());
        return AccountMapper.Map(account);
    }

    [HttpPost("login")]
    public async Task<LoginResultModel> Login([FromBody] LoginRequestModel requestModel) {
        var request = LoginRequestMapper.Map(requestModel);
        var result = await _accountAuthenticationService.LoginAsync(request);
        return LoginResultMapper.Map(result);
    }

    [HttpPost("register")]
    public async Task Register([FromBody] RegisterRequestModel requestModel) {
        var request = RegisterRequestMapper.Map(requestModel);
        await _accountAuthenticationService.RegisterAsync(request);
    }
}
    
    
