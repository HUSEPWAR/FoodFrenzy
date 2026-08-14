using System;
using System.Collections.Generic;
using System.Text;
using FoodFrenzy.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodFrenzy.Infrastructure.Persistence.Configurations;

public class RegionConfiguration : IEntityTypeConfiguration<Region>
{
    public void Configure(EntityTypeBuilder<Region> builder)
    {
        builder.ToTable("Regions");

        builder.HasKey(region => region.Id);

        builder.Property(region => region.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(region => region.Code)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(region => region.CountryId)
            .IsRequired();

        builder.Property(region => region.IsActive)
            .IsRequired();

        builder.Property(region => region.CreatedAt)
            .IsRequired();

        builder.Property(region => region.UpdatedAt);

        builder.Property(region => region.IsDeleted)
            .IsRequired();

        builder.HasIndex(region => new { region.CountryId, region.Code })
            .IsUnique();

        builder.HasOne(region => region.Country)
            .WithMany(country => country.Regions)
            .HasForeignKey(region => region.CountryId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}