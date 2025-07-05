namespace Familia.Domain.Shared
{
    public class Errors
    {
        public static class General
        {
            public static Error ValueIsInvalid(string? name = null)
            {
                var label = name ?? "value";

                return Error.Validation("value.is.invalid", $"{label} валидация не пройдена");
            }

            public static Error NotFound(Guid? id = null)
            {
                var forId = id == null ? "" : $" для Id '{id}'";
                return Error.NotFound("record.not.found", $"запись не найдена{forId}");
            }

            public static Error ValueIsRequired(string? name = null)
            {
                var label = name == null ? "" : " " + name + "";
                return Error.NotFound("length.is.invalid", $"невалидная{label}длина");
            }
            public static Error AlreadyExist()
            {
                return Error.Validation("record.already.exist", "Record already exist");
            }
        }
    }
}
