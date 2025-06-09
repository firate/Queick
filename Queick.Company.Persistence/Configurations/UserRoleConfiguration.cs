using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Queick.Company.Domain;

namespace Queick.Company.Persistence.Configurations;

public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
{
    public void Configure(EntityTypeBuilder<UserRole> builder)
    {
        builder.ToTable("UserRoles");
        
        builder.HasKey(r => new { r.UserId, r.RoleId });

        builder.HasOne(ur => ur.User)
            .WithMany(u=> u.UserRoles)
            .HasForeignKey(ur => ur.UserId);
        
        builder.HasOne(ur => ur.Role)
            .WithMany(r => r.UserRoles)
            .HasForeignKey(ur => ur.RoleId);
    }
}