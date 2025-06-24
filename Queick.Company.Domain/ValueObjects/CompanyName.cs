using Queick.Company.Domain.Common;

namespace Queick.Company.Domain.ValueObjects;

public class CompanyName : ValueObject
{
    public string Value { get; private set; }

    public CompanyName(string value)
    {
        // TODO: additional validations
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("Value cannot be null or whitespace.", nameof(value));
        }

        Value = value.Trim();
    }

    // for EF Core
    private CompanyName()
    {
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value.ToUpperInvariant();
    }

    public static CompanyName Create(string value) => new CompanyName(value);

    public static implicit operator string(CompanyName companyName) => companyName?.Value ?? string.Empty;
    public static explicit operator CompanyName(string value) => new CompanyName(value);
    public override string ToString() => Value;
}