namespace Queick.Company.Application.Authorization;

public static class Permissions
{
    public static class Company
    {
        public const string ManageAll = "Company.ManageAll";
        public const string Read = "Company.Read";
        public const string Write = "Company.Write";
        public const string Delete = "Company.Delete";
    }
    
    public static class Branch
    {
        public const string ManageAll = "Branch.ManageAll";
        public const string Read = "Branch.Read";
        public const string Write = "Branch.Write";
        public const string Delete = "Branch.Delete";
    }
    
    public static class User
    {
        public const string ManageAll = "User.ManageAll";
        public const string Read = "User.Read";
        public const string Write = "User.Write";
        public const string Delete = "User.Delete";
    }
    
    public static class Role
    {
        public const string ManageAll = "Role.ManageAll";
        public const string Read = "Role.Read";
        public const string Write = "Role.Write";
        public const string Delete = "Role.Delete";
    }
    
    // Helper method to get all permissions
    public static List<(string Code, string Name, string Category, string Description)> GetAllPermissions()
    {
        return new List<(string, string, string, string)>
        {
            // Company permissions
            (Company.ManageAll, "Manage All Company", "Company", "Full access to all company operations"),
            (Company.Read, "Read Company", "Company", "View company information"),
            (Company.Write, "Write Company", "Company", "Create and update company information"),
            (Company.Delete, "Delete Company", "Company", "Delete company records"),
            
            // Branch permissions
            (Branch.ManageAll, "Manage All Branch", "Branch", "Full access to all branch operations"),
            (Branch.Read, "Read Branch", "Branch", "View branch information"),
            (Branch.Write, "Write Branch", "Branch", "Create and update branch information"),
            (Branch.Delete, "Delete Branch", "Branch", "Delete branch records"),
            
            // User permissions
            (User.ManageAll, "Manage All User", "User", "Full access to all user operations"),
            (User.Read, "Read User", "User", "View user information"),
            (User.Write, "Write User", "User", "Create and update user information"),
            (User.Delete, "Delete User", "User", "Delete user records"),
            
            // Role permissions
            (Role.ManageAll, "Manage All Role", "Role", "Full access to all role operations"),
            (Role.Read, "Read Role", "Role", "View role information"),
            (Role.Write, "Write Role", "Role", "Create and update role information"),
            (Role.Delete, "Delete Role", "Role", "Delete role records"),
        };
    }
}