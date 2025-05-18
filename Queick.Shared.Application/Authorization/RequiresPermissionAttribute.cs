namespace Queick.Shared.Application.Authorization;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
public class RequiresPermissionAttribute : Attribute
{
    public string[] Permissions { get; }
    
    public RequiresPermissionAttribute(params string[] permissions)
    {
        Permissions = permissions;
    }
}