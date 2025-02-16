using ChatBotHub.WebApi.Application.Services;
using ChatBotHub.WebApi.Dto;
using ChatBotHub.WebApi.Factories;
using ChatBotHub.WebApi.Infrastructure.Domain;
using Microsoft.AspNetCore.Mvc;

namespace ChatBotHub.WebApi.Controllers;

public class UserController : BaseController<User> {
    private readonly IUserService _userService;

    public UserController(IUserService userService) : base(userService) {
        _userService = userService;
    }

    [HttpGet("me")]
    public async Task<UserDto> GetMe() {
        var user = await _userService.GetByIdAsync(GetUserId());
        if (user == null) {
            throw new UnauthorizedAccessException();
        }

        return UserFactory.CreateUserDto(user);
    }
}
