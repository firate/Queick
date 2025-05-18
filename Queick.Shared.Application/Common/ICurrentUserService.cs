namespace Queick.Shared.Application.Common;

public interface ICurrentUserService
{
    string? UserId { get; }
    IReadOnlyList<string> Permissions { get; }
}