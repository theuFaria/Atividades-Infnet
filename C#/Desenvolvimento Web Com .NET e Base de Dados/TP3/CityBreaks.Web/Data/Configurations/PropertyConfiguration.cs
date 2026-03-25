using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Property = CityBreaks.Web.Models.Property;

namespace CityBreaks.Web.Data.Configurations;

public class PropertyConfiguration : IEntityTypeConfiguration<Property>
{
    public void Configure(EntityTypeBuilder<Property> builder)
    {
        builder.Property(p => p.Name)
            .HasColumnName("Name")
            .HasMaxLength(100)
            .IsRequired();

        builder.HasOne(p => p.City)
            .WithMany(cn => cn.Properties)
            .HasForeignKey(c => c.CityId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasData(
            new Property { Id = 1, Name = "Hotel de mãe", PricePerNight = 2500, CityId = 1 },
            new Property { Id = 2, Name = "Copacabana Palace", PricePerNight = 1200, CityId = 1 },
            new Property { Id = 3, Name = "Hotel Paulista", PricePerNight = 500, CityId = 2 },
            new Property { Id = 4, Name = "Airbnb", PricePerNight = 100, CityId = 3 }
        );
    }
}