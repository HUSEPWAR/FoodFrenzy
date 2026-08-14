using System;
using System.Collections.Generic;

namespace FoodFrenzy.Domain.Entities;

public class Menu
{
    public Guid Id { get; set; }

    public Guid RestaurantBranchId { get; set; }

    public RestaurantBranch RestaurantBranch { get; set; } = null!;

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }

    public bool IsDeleted { get; set; } = false;

    public ICollection<MenuCategory> Categories { get; set; }
        = new List<MenuCategory>();

}