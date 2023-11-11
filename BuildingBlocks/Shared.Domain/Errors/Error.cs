namespace Shared.Domain.Errors
{
    public record Error(string Code, string Name)
    {
        public static Error None = new(string.Empty, string.Empty);

        public static Error NullValue = new("Error.NullValue", "Przekazano wartość null");

        public static Error InvalidRequest(string name) => new("Error.InvalidRequest", name);

        public static Error TaskFailed(string name) => new("Error.TaskFailed", name);

        public static Error NotFound(string name) => new("Error.NotFound", name);

        public static Error Exception(string name) => new("Error.Exception", name);

        public static Error ValidationError
            (IEnumerable<string> names) => new("Error.Validation", string.Join('\n', names));

        public override string ToString() => $"{Code}: {Name}";
    }
}