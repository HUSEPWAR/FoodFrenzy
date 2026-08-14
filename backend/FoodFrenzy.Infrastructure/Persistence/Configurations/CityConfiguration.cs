using System;
using System.Collections.Generic;
using System.Text;
using FoodFrenzy.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodFrenzy.Infrastructure.Persistence.Configurations;

public class CityConfiguration : IEntityTypeConfiguration<City>
{
    public void Configure(EntityTypeBuilder<City> builder)
    {
        builder.ToTable("Cities");

        builder.HasKey(city => city.Id);

        builder.Property(city => city.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(city => city.Code)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(city => city.RegionId)
            .IsRequired();

        builder.Property(city => city.IsActive)
            .IsRequired();

        builder.Property(city => city.CreatedAt)
            .IsRequired();

        builder.Property(city => city.UpdatedAt);

        builder.Property(city => city.IsDeleted)
            .IsRequired();

        builder.HasIndex(city => new { city.RegionId, city.Code })
            .IsUnique();

        builder.HasOne(city => city.Region)
    .WithMany(region => region.Cities)
    .HasForeignKey(city => city.RegionId)
    .OnDelete(DeleteBehavior.Restrict);
    }
}
