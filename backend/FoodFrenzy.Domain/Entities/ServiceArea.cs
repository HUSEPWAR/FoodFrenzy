using System;
using System.Collections.Generic;
using System.Text;

namespace FoodFrenzy.Domain.Entities;

public class ServiceArea
{
    public Guid Id { get; set; }

    public Guid CityId { get; set; }

    public City City { get; set; } = null!;

    public string Name { get; set; } = string.Empty;

    public string Code { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }

    public bool IsDeleted { get; set; } = false;

    public ICollection<Restaurant> Restaurants { get; set; }
    = new List<Restaurant>();

    public ICollection<RestaurantBranch> RestaurantBranches { get; set; }
        = new List<RestaurantBranch>();

}