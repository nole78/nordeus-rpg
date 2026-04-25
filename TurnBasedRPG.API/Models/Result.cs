using TurnBasedRPG.API.Enums;

namespace TurnBasedRPG.API.Models
{
    public class Result<T>
    {
        public bool IsSuccess { get; }
        public T? Value { get; }
        public string? Error { get; }
        public ErrorType? ErrorType { get; }

        protected Result(bool isSuccess, T? value, string? error, ErrorType? errorType)
        {
            IsSuccess = isSuccess;
            Value = value;
            Error = error;
            ErrorType = errorType;
        }

        public static Result<T> Success(T value)
            => new(true, value, null, null);

        public static Result<T> Failure(string error, ErrorType type)
            => new(false, default, error, type);
    }
}
