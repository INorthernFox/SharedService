using System.Text.Json.Serialization;

namespace SharedKernel;

public record Error
{
    public string Code { get; }

    public string Message { get; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public ErrorType Type { get; }

    public string? InvalidField { get; }

    [JsonConstructor]
    private Error(string code, string message, ErrorType type, string? invalidField)
    {
        Code = code;
        Message = message;
        Type = type;
        InvalidField = invalidField;
    }

    public static Error NotFound(string message, string? code = null)
        => new(code ?? ErrorCodes.RecordNotFound, message, ErrorType.NOT_FOUND, null);

    public static Error Validation(string message, string? code = null, string? invalidField = null)
        => new(code ?? ErrorCodes.ValidationFailed, message, ErrorType.VALIDATION, invalidField);

    public static Error Conflict(string message, string? code = null, string? invalidField = null)
        => new(code ?? ErrorCodes.ConflictOccurred, message, ErrorType.CONFLICT, invalidField);

    public static Error Failure(string message, string? code = null)
        => new(code ?? ErrorCodes.GeneralFailure, message, ErrorType.FAILURE, null);

    public static Error NotFoundWithId(string message, Guid id, string? code = null)
        => new(code ?? ErrorCodes.RecordNotFound, $"{message} (ID: {id})", ErrorType.NOT_FOUND, null);

    public static Error ValidationField(string message, string invalidField, string? code = null)
        => new(code ?? ErrorCodes.ValidationFailed, message, ErrorType.VALIDATION, invalidField);

    public Errors ToErrors() => this;
}