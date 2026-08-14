using System;
using System.Collections.Generic;
using System.Text;
using FoodFrenzy.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodFrenzy.Infrastructure.Persistence.Configurations;

public class MenuItemConfiguration
    : IEntityTypeConfiguration<MenuItem>
{
    public void Configure(EntityTypeBuilder<MenuItem> builder)
    {
        builder.ToTable("MenuItems");

        builder.HasKey(item => item.Id);

        builder.Property(item => item.MenuCategoryId)
            .IsRequired();

        builder.Property(item => item.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(item => item.Description)
            .HasMaxLength(2000);

        builder.Property(item => item.Price)
            .IsRequired()
            .HasPrecision(18, 2);

        builder.Property(item => item.IsAvailable)
            .IsRequired();

        builder.Property(item => item.DisplayOrder)
            .IsRequired();

        builder.Property(item => item.IsActive)
            .IsRequired();

        builder.Property(item => item.CreatedAt)
            .IsRequired();

        builder.Property(item => item.UpdatedAt);

        builder.Property(item => item.IsDeleted)
            .IsRequired();

        builder.HasOne(item => item.MenuCategory)
            .WithMany(category => category.Items)
            .HasForeignKey(item => item.MenuCategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(item => item.MenuCategoryId);

        builder.HasIndex(item => new
        {
            item.MenuCategoryId,
            item.Name
        })
        .IsUnique();

        builder.HasIndex(item => new
        {
            item.MenuCategoryId,
            item.DisplayOrder
        });
    }
}