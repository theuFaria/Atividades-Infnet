using CityBreaks.Web.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CityBreaks.Web.Data.Configurations;

public class CityConfiguration : IEntityTypeConfiguration<City>
{
    public void Configure(EntityTypeBuilder<City> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Name)
            .HasColumnName("Name")
            .HasMaxLength(100)
            .IsRequired();

        builder.HasOne(c => c.Country)
            .WithMany(cn => cn.Cities)
            .HasForeignKey(c => c.CountryId);

        builder.HasData(
            new City { Id = 1, Name = "Rio de Janeiro", CountryId = 1 },
            new City { Id = 2, Name = "São Paulo", CountryId = 1 },
            new City { Id = 3, Name = "Porto", CountryId = 2 }
        );
    }
}