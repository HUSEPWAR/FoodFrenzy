using System;
using System.Collections.Generic;
using System.Text;
using FoodFrenzy.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodFrenzy.Infrastructure.Persistence.Configurations;

public class MenuCategoryConfiguration
    : IEntityTypeConfiguration<MenuCategory>
{
    public void Configure(EntityTypeBuilder<MenuCategory> builder)
    {
        builder.ToTable("MenuCategories");

        builder.HasKey(category => category.Id);

        builder.Property(category => category.MenuId)
            .IsRequired();

        builder.Property(category => category.Name)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(category => category.Description)
            .HasMaxLength(1000);

        builder.Property(category => category.DisplayOrder)
            .IsRequired();

        builder.Property(category => category.IsActive)
            .IsRequired();

        builder.Property(category => category.CreatedAt)
            .IsRequired();

        builder.Property(category => category.UpdatedAt);

        builder.Property(category => category.IsDeleted)
            .IsRequired();

        builder.HasOne(category => category.Menu)
            .WithMany(menu => menu.Categories)
            .HasForeignKey(category => category.MenuId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(category => category.MenuId);

        builder.HasIndex(category => new
        {
            category.MenuId,
            category.Name
        })
        .IsUnique();

        builder.HasIndex(category => new
        {
            category.MenuId,
            category.DisplayOrder
        });
    }
}