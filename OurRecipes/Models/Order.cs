using System;
using System.Collections.Generic;

namespace OurRecipes.Models;

public partial class Order
{
    public decimal OrderId { get; set; }

    public string? OrderStatus { get; set; }

    public decimal? UserId { get; set; }

    public decimal? RecipeId { get; set; }

    public decimal? OrderPrice { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public virtual Recipe? Recipe { get; set; }

    public virtual User? User { get; set; }
}
