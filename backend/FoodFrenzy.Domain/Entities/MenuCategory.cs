using System;
using System.Collections.Generic;
using System.Text;

namespace FoodFrenzy.Domain.Entities;

public class MenuCategory
{
    public Guid Id { get; set; }

    public Guid MenuId { get; set; }

    public Menu Menu { get; set; } = null!;

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public int DisplayOrder { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }

    public bool IsDeleted { get; set; } = false;

    public ICollection<MenuItem> Items { get; set; }
        = new List<MenuItem>();

}