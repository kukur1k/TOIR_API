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

public record TableOrderResponse(
    string Id,
    string Eq,
    string ColorCodePriority,
    string Status,
    string StatusColor,
    string Brigade,
    string DateStart,
    string Reason,
    string Master
);