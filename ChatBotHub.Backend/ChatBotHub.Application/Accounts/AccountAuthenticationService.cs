using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using ChatBotHub.Application.Accounts.Exceptions;
using ChatBotHub.Application.Accounts.Requests;
using ChatBotHub.Application.Accounts.Results;
using ChatBotHub.Application.Common.Options;
using ChatBotHub.Domain.Accounts;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace ChatBotHub.Application.Accounts;

public class AccountAuthenticationService : IAccountAuthenticationService {

    private readonly IAccountQueryService _accountQueryService;
    private readonly IAccountCommandService _accountCommandService;
    private readonly JwtOptions _jwtOptions;

    public AccountAuthenticationService(
        IAccountQueryService accountQueryService,
        IAccountCommandService accountCommandService,
        IOptions<JwtOptions> jwtOptions
    ) {
        _accountQueryService = accountQueryService;
        _accountCommandService = accountCommandService;
        _jwtOptions = jwtOptions.Value;
    }

    public async Task<LoginResult> LoginAsync(LoginRequest request) {
        var account = await _accountQueryService.GetByEmailAsync(request.Email);
        if (account is null) {
            throw new AccountNotFoundException(request.Email);
        }

        var passwordHash = GetPasswordHash(request.Password);
        if (passwordHash != account.HashPassword) {
            throw new InvalidPasswordException();
        }

        return new LoginResult {
            Token = GenerateJwt(account)
        };
    }

    public async Task RegisterAsync(RegisterRequest request) {
        var account = await _accountQueryService.GetByEmailAsync(request.Email);
        if (account is not null) {
            throw new AccountAlreadyExistsException(request.Email);
        }

        var hashedPassword = GetPasswordHash(request.Password);
        var saveAccountRequest = new SaveAccountRequest {
            Name = request.Name,
            LastName = request.LastName,
            Email = request.Email,
            HashPassword = hashedPassword
        };

        await _accountCommandService.CreateAsync(saveAccountRequest);
    }
    
    private string GetPasswordHash(string password) {
        var sha = SHA256.Create();
        var byteArray = Encoding.UTF8.GetBytes(password);
        var hash = sha.ComputeHash(byteArray);
        return Convert.ToBase64String(hash);
    }
    
    private string GenerateJwt(Account account) {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Key));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier,account.Id.ToString()),
            new Claim(ClaimTypes.Email,account.Email)
        };
        
        var token = new JwtSecurityToken(
            _jwtOptions.Issuer,
            _jwtOptions.Audience,
            claims,
            expires: DateTime.Now.AddDays(7),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
