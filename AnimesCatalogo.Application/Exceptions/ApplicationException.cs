public class ApplicationException : Exception
{
    public ApplicationException(string message) : base(message) { }

    public ApplicationException(string message, Exception innerException) : base(message, innerException) { }

    public ApplicationException(ErrorCode errorCode, string message) : base(message)
    {
        ErrorCode = errorCode;
    }

    public ApplicationException(ErrorCode errorCode, string message, Exception innerException) : base(message, innerException)
    {
        ErrorCode = errorCode;
    }

    public ErrorCode ErrorCode { get; }
}

public enum ErrorCode
{
    InvalidData,
    ResourceNotFound,
    DuplicateEntry,
    UnauthorizedAccess,
    InternalServerError
}