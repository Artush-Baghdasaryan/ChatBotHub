namespace ChatBotHub.WebApi.Infrastructure.Domain;

public class User : BaseModel {
    public required string Username { get; set; }
    public required string Email { get; set; }
    public required string HashPassword { get; set; }
}

