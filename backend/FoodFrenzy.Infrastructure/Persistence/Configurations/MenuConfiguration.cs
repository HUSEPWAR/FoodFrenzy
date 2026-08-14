using System;
using System.Collections.Generic;
using System.Text;
using FoodFrenzy.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodFrenzy.Infrastructure.Persistence.Configurations;

public class MenuConfiguration : IEntityTypeConfiguration<Menu>
{
    public void Configure(EntityTypeBuilder<Menu> builder)
    {
        builder.ToTable("Menus");

        builder.HasKey(menu => menu.Id);

        builder.Property(menu => menu.RestaurantBranchId)
            .IsRequired();

        builder.Property(menu => menu.Name)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(menu => menu.Description)
            .HasMaxLength(2000);

        builder.Property(menu => menu.IsActive)
            .IsRequired();

        builder.Property(menu => menu.CreatedAt)
            .IsRequired();

        builder.Property(menu => menu.UpdatedAt);

        builder.Property(menu => menu.IsDeleted)
            .IsRequired();

        builder.HasOne(menu => menu.RestaurantBranch)
            .WithMany(branch => branch.Menus)
            .HasForeignKey(menu => menu.RestaurantBranchId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(menu => menu.RestaurantBranchId);

        builder.HasIndex(menu => new
        {
            menu.RestaurantBranchId,
            menu.Name
        })
        .IsUnique();
    }
}
