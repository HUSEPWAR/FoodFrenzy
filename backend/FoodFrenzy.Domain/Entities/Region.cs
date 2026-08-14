using System;
using System.Collections.Generic;
using System.Text;

namespace FoodFrenzy.Domain.Entities;

public class Region
{
    public Guid Id { get; set; }

    public Guid CountryId { get; set; }

    public Country Country { get; set; } = null!;

    public ICollection<City> Cities { get; set; } = new List<City>();

    public string Name { get; set; } = string.Empty;

    public string Code { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }

    public bool IsDeleted { get; set; } = false;

    
}