
namespace Application.Models.Requests;

public class RegisterRequestModel {
    public required string Username { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
}
