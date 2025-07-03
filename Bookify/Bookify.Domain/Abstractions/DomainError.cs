namespace Bookify.Domain.Abstractions;

public record DomainError(string Code, string Name)
{
    public static readonly DomainError None = new(string.Empty, string.Empty);

    public static readonly DomainError NullValue = new("Error.NullValue", "Null value was provided");
}