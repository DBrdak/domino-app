using Shared.Domain.Exceptions;

namespace Shared.Domain.ResponseTypes;

/// <summary>
/// Can be treated as a regular Int32.
/// </summary>
public sealed record PageSize
{
    private readonly int _value;

    private PageSize(int value) => _value = value;

    public static implicit operator PageSize(int value)
    {
        if (value <= 0)
        {
            throw new DomainException<PageSize>("Page size must be greater than 0");
        }

        return new (value);
    }

    public static implicit operator int(PageSize pageSize) => pageSize._value;
}