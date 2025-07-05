namespace Familia.Domain.Shared
{
    public record Error
    {
        public string Code { get; }
        public string Message { get; }
        public ErrorType Type { get; }

        private Error(string code, string message, ErrorType type)
        {
            Code = code;
            Message = message;
            Type = type;
        }

        public static Error Validation(string Code, string Message) =>
            new Error(Code, Message, ErrorType.Validation);

        public static Error NotFound(string Code, string Message) =>
            new Error(Code, Message, ErrorType.NotFound);

        public static Error Failure(string Code, string Message) =>
            new Error(Code, Message, ErrorType.Failure);

        public static Error Conflict(string Code, string Message) =>
            new Error(Code, Message, ErrorType.Conflict);
    }

    public enum ErrorType
    {
        Validation,
        NotFound,
        Failure,
        Conflict
    }
}
