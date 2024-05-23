using System;
using System.Collections.Generic;

namespace OurRecipes.Models;

public partial class IngredientUnit
{
    public decimal IngredientUnitId { get; set; }

    public string? IngredientUnitName { get; set; }

    public string? IngredientUnitType { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public virtual ICollection<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
}
