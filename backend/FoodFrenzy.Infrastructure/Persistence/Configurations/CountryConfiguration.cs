using System;
using System.Collections.Generic;
using System.Text;
using FoodFrenzy.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodFrenzy.Infrastructure.Persistence.Configurations;

public class CountryConfiguration : IEntityTypeConfiguration<Country>
{
    public void Configure(EntityTypeBuilder<Country> builder)
    {
        builder.ToTable("Countries");

        builder.HasKey(country => country.Id);

        builder.Property(country => country.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(country => country.Code)
            .IsRequired()
            .HasMaxLength(10);

        builder.Property(country => country.CurrencyCode)
            .IsRequired()
            .HasMaxLength(10);

        builder.Property(country => country.IsActive)
            .IsRequired();

        builder.Property(country => country.CreatedAt)
            .IsRequired();

        builder.Property(country => country.UpdatedAt);

        builder.Property(country => country.IsDeleted)
            .IsRequired();

        builder.HasIndex(country => country.Code)
            .IsUnique();
    }
}

