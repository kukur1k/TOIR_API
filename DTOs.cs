namespace TOIR_API;


public record LoginRequest(string Email, string Password);
public record RegisterRequest(
    string Email,
    string Password,
    string Phone,
    string LastName,
    string FirstName,
    string? MiddleName);

public record AuthResponce(
    string AccessToken,
    string RefreshToken,
    string Role
);

public record RefreshRequest(
    string RefreshToken
);