namespace TOIR_API;

public record ApiResponse<T>(
    bool Success,
    T? Data,
    string Message,
    string? ErrorCode = null
);


public static class Api
{
    public static IResult Ok<T>(T data, string message = "OK") =>
        Results.Ok(new ApiResponse<T>(true, data, message));

    public static IResult Created<T>(T data, string uri) =>
        Results.Created(uri, new ApiResponse<T>(true, data, "Создано"));

    public static IResult Fail(string message, int status, string errorCode) =>
        Results.Json(new ApiResponse<object>(false, null, message, errorCode), statusCode: status);

    public static IResult NoContent(string message) =>
        Fail(message, 204, "NO_CONTENT");

    public static IResult BadRequest(string message) =>
        Fail(message, 400, "BAD_REQUEST");

    public static IResult Unauthorized() =>
        Fail("Не авторизован", 401, "UNAUTHORIZED");

    public static IResult Forbidden() =>
        Fail("Не достаточно прав", 403, "FORBIDDEN");

    public static IResult NotFound(string message) =>
        Fail(message, 404, "NOT_FOUND");

    public static IResult Conflict(string message) =>
        Fail(message, 409, "CONFLICT");

    public static IResult TooManyRequest(string message) =>
        Fail(message, 429, "TOO_MANY_REQUEST");

    public static IResult InternalServerError(string message) =>
        Fail(message, 500, "INTERNAL_SERVER_ERROR");
}