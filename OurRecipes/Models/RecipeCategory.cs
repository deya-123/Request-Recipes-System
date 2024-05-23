using System;
using System.Collections.Generic;

namespace OurRecipes.Models;

public partial class RecipeCategory
{
    public decimal CategoryId { get; set; }

    public string? CategoryName { get; set; }

    public decimal? CategoryTypeId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public string? CategoryImage { get; set; }

    public virtual RecipeCategoryType? CategoryType { get; set; }

    public virtual ICollection<Recipe> Recipes { get; set; } = new List<Recipe>();
}
