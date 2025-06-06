using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Queick.Company.Domain;

namespace Queick.Company.Persistence.Configurations;

public class AddressConfiguration : IEntityTypeConfiguration<Address>
{
    public void Configure(EntityTypeBuilder<Address> builder)
    {
        builder.Property(b => b.Latitude)
            .HasColumnName("Latitude")
            .HasColumnType("decimal(10,8)");
        
        builder.Property(b => b.Longitude)
            .HasColumnName("Longitude")
            .HasColumnType("decimal(11,8)");
    }
}