using System;
using System.Collections.Generic;
using System.Text;

namespace FoodFrenzy.Domain.Entities;

public class Restaurant
{
    public Guid Id { get; set; }

    public Guid ServiceAreaId { get; set; }

    public ServiceArea ServiceArea { get; set; } = null!;

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string ContactEmail { get; set; } = string.Empty;

    public string ContactPhone { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }

    public bool IsDeleted { get; set; } = false;

    public ICollection<RestaurantBranch> Branches { get; set; }
        = new List<RestaurantBranch>();
}