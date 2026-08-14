using System;
using System.Collections.Generic;
using System.Text;
using FoodFrenzy.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodFrenzy.Infrastructure.Persistence.Configurations;

public class RestaurantConfiguration : IEntityTypeConfiguration<Restaurant>
{
    public void Configure(EntityTypeBuilder<Restaurant> builder)
    {
        builder.ToTable("Restaurants");

        builder.HasKey(restaurant => restaurant.Id);

        builder.Property(restaurant => restaurant.ServiceAreaId)
            .IsRequired();

        builder.Property(restaurant => restaurant.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(restaurant => restaurant.Description)
            .HasMaxLength(2000);

        builder.Property(restaurant => restaurant.ContactEmail)
            .HasMaxLength(255);

        builder.Property(restaurant => restaurant.ContactPhone)
            .HasMaxLength(30);

        builder.Property(restaurant => restaurant.IsActive)
            .IsRequired();

        builder.Property(restaurant => restaurant.CreatedAt)
            .IsRequired();

        builder.Property(restaurant => restaurant.UpdatedAt);

        builder.Property(restaurant => restaurant.IsDeleted)
            .IsRequired();

        builder.HasOne(restaurant => restaurant.ServiceArea)
            .WithMany(serviceArea => serviceArea.Restaurants)
            .HasForeignKey(restaurant => restaurant.ServiceAreaId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(restaurant => restaurant.ServiceAreaId);
    }
}