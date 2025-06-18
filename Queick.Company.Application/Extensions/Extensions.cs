namespace Queick.Company.Application.Extensions;

public class Extensions
{
    
}


public static class StringExtensions
{
    public static Guid ToGuid(this string? guidString)
    {
        return Guid.TryParse(guidString, out Guid result) ? result : Guid.Empty;
    }
}