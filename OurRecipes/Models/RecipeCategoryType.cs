using System;
using System.Collections.Generic;

namespace OurRecipes.Models;

public partial class RecipeCategoryType
{
    public decimal RecipeCategoryTypeId { get; set; }

    public string? RecipeCategoryTypeName { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<RecipeCategory> RecipeCategories { get; set; } = new List<RecipeCategory>();
}
