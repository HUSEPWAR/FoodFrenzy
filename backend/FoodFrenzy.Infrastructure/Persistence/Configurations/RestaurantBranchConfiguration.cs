using System;
using System.Collections.Generic;
using System.Text;
using FoodFrenzy.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodFrenzy.Infrastructure.Persistence.Configurations;

public class RestaurantBranchConfiguration
    : IEntityTypeConfiguration<RestaurantBranch>
{
    public void Configure(EntityTypeBuilder<RestaurantBranch> builder)
    {
        builder.ToTable("RestaurantBranches");

        builder.HasKey(branch => branch.Id);

        builder.Property(branch => branch.RestaurantId)
            .IsRequired();

        builder.Property(branch => branch.ServiceAreaId)
            .IsRequired();

        builder.Property(branch => branch.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(branch => branch.AddressLine1)
            .IsRequired()
            .HasMaxLength(250);

        builder.Property(branch => branch.AddressLine2)
            .HasMaxLength(250);

        builder.Property(branch => branch.PostalCode)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(branch => branch.Latitude)
            .HasPrecision(9, 6);

        builder.Property(branch => branch.Longitude)
            .HasPrecision(9, 6);

        builder.Property(branch => branch.ContactPhone)
            .HasMaxLength(30);

        builder.Property(branch => branch.IsActive)
            .IsRequired();

        builder.Property(branch => branch.CreatedAt)
            .IsRequired();

        builder.Property(branch => branch.UpdatedAt);

        builder.Property(branch => branch.IsDeleted)
            .IsRequired();

        builder.HasOne(branch => branch.Restaurant)
            .WithMany(restaurant => restaurant.Branches)
            .HasForeignKey(branch => branch.RestaurantId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(branch => branch.ServiceArea)
            .WithMany(serviceArea => serviceArea.RestaurantBranches)
            .HasForeignKey(branch => branch.ServiceAreaId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(branch => branch.RestaurantId);

        builder.HasIndex(branch => branch.ServiceAreaId);

        builder.HasIndex(branch => new
        {
            branch.RestaurantId,
            branch.Name
        })
        .IsUnique();
    }
}
