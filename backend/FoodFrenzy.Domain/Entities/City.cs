using System;
using System.Collections.Generic;
using System.Text;

namespace FoodFrenzy.Domain.Entities;

public class City
{
    public Guid Id { get; set; }

    public Guid RegionId { get; set; }

    public Region Region { get; set; } = null!;

    public string Name { get; set; } = string.Empty;

    public string Code { get; set; } = string.Empty;

    public string TimeZone { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }

    public bool IsDeleted { get; set; } = false;

    public ICollection<ServiceArea> ServiceAreas { get; set; } = new List<ServiceArea>();
}
