namespace Queick.Company.Application.Common;

public interface ICurrentUserService
{
    string GetCurrentUserId();
    List<string> GetCurrentUserPermissions();
    bool HasPermission(string permission);
    bool HasAnyPermission(params string[] permissions);
    bool HasAllPermissions(params string[] permissions);
}
