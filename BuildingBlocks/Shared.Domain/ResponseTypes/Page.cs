using Shared.Domain.Exceptions;

namespace Shared.Domain.ResponseTypes;

/// <summary>
/// Can be treated as a regular Int32
/// </summary>
public sealed record Page
{
    private readonly int _value;

    private Page(int value) => _value = value;

    public static implicit operator Page(int value)
    {
        if (value <= 0)
        {
            throw new DomainException<Page>("Page must be greater than 0");
        }

        return new (value);
    }

    public static implicit operator int(Page page) => page._value;
}