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
    string Priority,
    string ColorCodePriority,
    string Status,
    string StatusColor,
    string Brigade,
    string DateStart,
    string Reason,
    string Master
);


public record ScheduleToResponse(
        int EqId, string EqTitle, string TypeTo, string PlanDate, string Status, string Color
    );

    public record HistoryTo(
        int Id,
        string ToTitle,
        string Date
    );

    // public record EqRequest(
    //     string? InvNumber,
    //     string? Name, string? Model,
    //     string? SerialNumber, DateOnly? ExpStart,
    //     DateOnly? WorkStart, string? Manifacture,
    //     DateOnly? WarrancyDate, string? Workshop,
    //     string? Status
    // );

    public class EqRequest
{
    public int Id { get; set; }
    public string? InventNumber { get; set; }
    public string? Name { get; set; }
    public string? Model { get; set; }
    public DateOnly? DateStartWork { get; set; }
    public DateOnly? DateStartExp { get; set; }
    public string? Manufacture { get; set; }
    public DateOnly? WarrancyTime { get; set; }
    public string? SerialNumber { get; set; }
    public string? Workshop { get; set; }
    public string? EqStatus { get; set; }
}

    public record TypeToRequest(string? Title, string? Description, string? Interval);

    public record sheduleFiltersRequest(string? status, string? typeTo, DateOnly? dateStart, DateOnly? dateEnd);

    public record SheduleToRequest(DateOnly Date, int ToId, int BrigadeId, int EqId);

    public record WorkOrderRequest(string TypeWo, string Priority, string StatusWo, string Description, string? Reason, DateTime dateStart, DateTime dateEnd);