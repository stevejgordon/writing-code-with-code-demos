namespace WritingCodeWithCode.Specification;

public sealed class Schema
{
    public IReadOnlyCollection<SchemaType> Types { get; init; } = Array.Empty<SchemaType>();
}

public sealed class SchemaType
{
    public string TypeName { get; init; } = string.Empty;
    public IReadOnlyCollection<SchemaProperty> Properties { get; init; } = Array.Empty<SchemaProperty>();
}

public sealed class SchemaProperty
{
    public string Type { get; init; } = string.Empty;
    public string Name { get; init; } = string.Empty;
}
