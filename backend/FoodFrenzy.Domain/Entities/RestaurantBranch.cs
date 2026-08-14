using System;
using System.Collections.Generic;
using System.Text;

namespace FoodFrenzy.Domain.Entities;

public class RestaurantBranch
{
    public Guid Id { get; set; }

    public Guid RestaurantId { get; set; }

    public Restaurant Restaurant { get; set; } = null!;

    public Guid ServiceAreaId { get; set; }

    public ServiceArea ServiceArea { get; set; } = null!;

    public string Name { get; set; } = string.Empty;

    public string AddressLine1 { get; set; } = string.Empty;

    public string AddressLine2 { get; set; } = string.Empty;

    public string PostalCode { get; set; } = string.Empty;

    public decimal Latitude { get; set; }

    public decimal Longitude { get; set; }

    public string ContactPhone { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }

    public bool IsDeleted { get; set; } = false;

    public ICollection<Menu> Menus { get; set; }
        = new List<Menu>();
}
