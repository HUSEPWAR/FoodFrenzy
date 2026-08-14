using FoodFrenzy.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FoodFrenzy.Infrastructure.Persistence;

public class FoodFrenzyDbContext : DbContext
{
    public FoodFrenzyDbContext(
        DbContextOptions<FoodFrenzyDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();

    public DbSet<Country> Countries => Set<Country>();

    public DbSet<Region> Regions => Set<Region>();

    public DbSet<City> Cities => Set<City>();

    public DbSet<ServiceArea> ServiceAreas => Set<ServiceArea>();

    public DbSet<Restaurant> Restaurants => Set<Restaurant>();

    public DbSet<RestaurantBranch> RestaurantBranches => Set<RestaurantBranch>();

    public DbSet<Menu> Menus => Set<Menu>();

    public DbSet<MenuCategory> MenuCategories => Set<MenuCategory>();

    public DbSet<MenuItem> MenuItems => Set<MenuItem>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(FoodFrenzyDbContext).Assembly);
    }
}

/*
 
using System;
using System.Collections.Generic;
using System.Text;
using FoodFrenzy.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FoodFrenzy.Infrastructure.Persistence;

public class FoodFrenzyDbContext : DbContext
{
    public FoodFrenzyDbContext(
        DbContextOptions<FoodFrenzyDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(x => x.Id);

            entity.Property(x => x.FirstName)
                .HasMaxLength(100)
                .IsRequired();

            entity.Property(x => x.LastName)
                .HasMaxLength(100)
                .IsRequired();

            entity.Property(x => x.Email)
                .HasMaxLength(255)
                .IsRequired();

            entity.HasIndex(x => x.Email)
                .IsUnique();

            entity.Property(x => x.PasswordHash)
                .IsRequired();

            entity.Property(x => x.PhoneNumber)
                .HasMaxLength(20);
        });
    }
}

*/
