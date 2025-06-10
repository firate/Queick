using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Queick.Company.Application.Authorization;
using Queick.Company.Domain;

namespace Queick.Company.Persistence.Seeder;

public class DatabaseSeeder
{
    private readonly ApplicationDbContext _context;
    private readonly IPasswordHasher<User> _passwordHashService;
    private readonly IConfiguration _configuration;
    private readonly ILogger<DatabaseSeeder> _logger;

    public DatabaseSeeder(
        ApplicationDbContext context,
        IConfiguration configuration,
        ILogger<DatabaseSeeder> logger,
        IPasswordHasher<User> passwordHashService)
    {
        _context = context;

        _configuration = configuration;
        _logger = logger;
        _passwordHashService = passwordHashService;
    }

    public async Task SeedAsync()
    {
        try
        {
            await SeedPermissionsAsync();
            await SeedRolesAsync();
            await SeedDefaultUsersAsync();

            _logger.LogInformation("Database seeding completed successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database");
            throw;
        }
    }

    private async Task SeedPermissionsAsync()
    {
        var allPermissions = Permissions.GetAllPermissions();

        foreach (var (code, name, category, description) in allPermissions)
        {
            var exists = await _context.Permissions.AnyAsync(p => p.Code == code);
            if (exists) continue;
            _context.Permissions.Add(new Permission
            {
                Code = code,
                Name = name,
                Category = category,
                Description = description
            });
        }

        await _context.SaveChangesAsync();
        _logger.LogInformation($"Permission seeding completed. Total permissions: {allPermissions.Count}");
    }

    private async Task SeedRolesAsync()
    {
        // Admin Role
        if (!await _context.Roles.AnyAsync(r => r.Name == "Administrator"))
        {
            var adminRole = new Role
            {
                Name = "Administrator",
                Description = "System administrator with full access",
                IsActive = true
            };

            _context.Roles.Add(adminRole);
            await _context.SaveChangesAsync();

            // Assign all permissions to admin role
            var allPermissions = await _context.Permissions.ToListAsync();
            foreach (var permission in allPermissions)
            {
                _context.RolePermissions.Add(new RolePermission
                {
                    RoleId = adminRole.Id,
                    PermissionId = permission.Id
                });
            }

            await _context.SaveChangesAsync();
            _logger.LogInformation("Administrator role created with all permissions");
        }
    }

    private async Task SeedDefaultUsersAsync()
    {
        _logger.LogInformation("Starting user seeding...");

        // Get admin credentials from configuration
        var adminEmail = _configuration["DefaultAdmin:Email"] ?? "admin@queick.com";
        var adminUsername = _configuration["DefaultAdmin:Username"] ?? "admin";
        var adminPassword = _configuration["DefaultAdmin:Password"] ?? "Admin123!";

        // Create admin user if not exists
        if (!await _context.Users.AnyAsync(u => u.Username == adminUsername))
        {
            var adminUser = new User
            {
                Username = adminUsername,
                Email = adminEmail,
                IsActive = true
            };
            var passwordHash = _passwordHashService.HashPassword(adminUser, adminPassword);
            adminUser.PasswordHash = passwordHash;
            
            _context.Users.Add(adminUser);
            await _context.SaveChangesAsync();

            // Assign Administrator role
            var adminRole = await _context.Roles.FirstOrDefaultAsync(r => r.Name == "Administrator");
            if (adminRole != null)
            {
                _context.UserRoles.Add(new UserRole
                {
                    UserId = adminUser.Id,
                    RoleId = adminRole.Id
                });
                await _context.SaveChangesAsync();
            }
        }
    }
}