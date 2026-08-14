using System;
using System.Collections.Generic;
using System.Text;

namespace FoodFrenzy.Domain.Entities;

public class MenuItem
{
    public Guid Id { get; set; }

    public Guid MenuCategoryId { get; set; }

    public MenuCategory MenuCategory { get; set; } = null!;

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public bool IsAvailable { get; set; } = true;

    public int DisplayOrder { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }

    public bool IsDeleted { get; set; } = false;
}
