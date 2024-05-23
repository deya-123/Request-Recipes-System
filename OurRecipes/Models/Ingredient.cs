using System;
using System.Collections.Generic;

namespace OurRecipes.Models;

public partial class Ingredient
{
    public decimal IngredientId { get; set; }

    public string? IngredientCustomName { get; set; }

    public string? IngredientQuantity { get; set; }

    public decimal? IngredientUnitId { get; set; }

    public decimal? RecipeId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public virtual IngredientUnit? IngredientUnit { get; set; }

    public virtual Recipe? Recipe { get; set; }
}
