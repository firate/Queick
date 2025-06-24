using Queick.Company.Domain.Common;

namespace Queick.Company.Domain.ValueObjects;

public class CompanyName:ValueObject
{
    public string Value { get; set; }
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}