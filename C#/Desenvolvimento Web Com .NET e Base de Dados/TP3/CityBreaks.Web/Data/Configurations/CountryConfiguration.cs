using CityBreaks.Web.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CityBreaks.Web.Data.Configurations;

public class CountryConfiguration : IEntityTypeConfiguration<Country>
{
    public void Configure(EntityTypeBuilder<Country> builder)
    {
        builder.Property(cn => cn.CountryName)
            .HasColumnName("CountryName")
            .HasMaxLength(100)
            .IsRequired();

        builder.HasData(
            new Country { Id = 1, CountryName = "Brasil", CountryCode = "BR" },
            new Country { Id = 2, CountryName = "Portugal", CountryCode = "PT" }
        );
    }
}