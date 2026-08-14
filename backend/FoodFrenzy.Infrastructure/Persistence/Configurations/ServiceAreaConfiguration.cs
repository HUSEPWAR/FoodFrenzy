using System;
using System.Collections.Generic;
using System.Text;
using FoodFrenzy.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodFrenzy.Infrastructure.Persistence.Configurations;

public class ServiceAreaConfiguration : IEntityTypeConfiguration<ServiceArea>
{
    public void Configure(EntityTypeBuilder<ServiceArea> builder)
    {
        builder.ToTable("ServiceAreas");

        builder.HasKey(serviceArea => serviceArea.Id);

        builder.Property(serviceArea => serviceArea.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(serviceArea => serviceArea.Code)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(serviceArea => serviceArea.CityId)
            .IsRequired();

        builder.Property(serviceArea => serviceArea.IsActive)
            .IsRequired();

        builder.Property(serviceArea => serviceArea.CreatedAt)
            .IsRequired();

        builder.Property(serviceArea => serviceArea.UpdatedAt);

        builder.Property(serviceArea => serviceArea.IsDeleted)
            .IsRequired();

        builder.HasIndex(serviceArea => new
        {
            serviceArea.CityId,
            serviceArea.Code
        })
        .IsUnique();

        builder.HasOne(serviceArea => serviceArea.City)
            .WithMany(city => city.ServiceAreas)
            .HasForeignKey(serviceArea => serviceArea.CityId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}