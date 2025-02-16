using Application.Models.Requests;
using Application.Models.Responses;

namespace ChatBotHub.WebApi.Application.Services;

public interface IAuthService {
    Task<LoginResultModel> LoginAsync(LoginRequestModel request );
    Task<bool> RegisterAsync(RegisterRequestModel request);
}
