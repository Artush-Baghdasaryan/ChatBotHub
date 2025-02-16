using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Application.Models.Requests;
using Application.Models.Responses;
using ChatBotHub.WebApi.Infrastructure.Domain;
using Infrastructure.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace ChatBotHub.WebApi.Application.Services.Implementations;

public class AuthService : IAuthService {
    private readonly IUserService _userService;
    private readonly JwtSettings _jwtSettings;

    public AuthService(
        IUserService userService,
        IOptions<JwtSettings> jwtSettings
    ) {
        _userService = userService;
        _jwtSettings = jwtSettings.Value;
    }

    public async Task<LoginResultModel> LoginAsync(LoginRequestModel request) {
        var user = await _userService.GetByEmailAsync(request.Email);
        if (user == null) {
            throw new Exception("User not found");
        }

        if (GetPasswordHash(request.Password) != user.HashPassword) {
            throw new Exception("Invalid password");
        }

        var loginResult = new LoginResultModel {
            Token = GenerateJwt(user)
        };

        return loginResult;
    }

    public async Task<bool> RegisterAsync(RegisterRequestModel request) {
        var user = await _userService.GetByEmailAsync(request.Email);
        if (user != null) {
            throw new Exception("User already exists");
        }

        var newUser = new User {
            Username = request.Username,
            Email = request.Email,
            HashPassword = GetPasswordHash(request.Password)
        };

        var userId = await _userService.CreateAsync(newUser);
        return userId != Guid.Empty;
    }

    private string GenerateJwt(User user) {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
            new Claim(ClaimTypes.Email,user.Email)
        };
        
        var token = new JwtSecurityToken(
            _jwtSettings.Issuer,
            _jwtSettings.Audience,
            claims,
            expires: DateTime.Now.AddDays(7),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private string GetPasswordHash(string password) {
        var sha = SHA256.Create();
        var byteArray = Encoding.UTF8.GetBytes(password);
        var hash = sha.ComputeHash(byteArray);
        return Convert.ToBase64String(hash);
    }
}