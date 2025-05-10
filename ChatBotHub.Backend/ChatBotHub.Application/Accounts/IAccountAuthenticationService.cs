using ChatBotHub.Application.Accounts.Requests;
using ChatBotHub.Application.Accounts.Results;

namespace ChatBotHub.Application.Accounts;

public interface IAccountAuthenticationService {
    Task<LoginResult> LoginAsync(LoginRequest request);
    Task RegisterAsync(RegisterRequest request);
}
