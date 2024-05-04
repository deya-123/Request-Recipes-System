using System;
using System.Collections.Generic;

namespace OurRecipes.Models;

public partial class Recipe
{
    public decimal RecipeId { get; set; }

    public string? RecipeName { get; set; }

    public decimal? RecipePrice { get; set; }

    public string? RecipeMainImgPath { get; set; }

    public string? RecipeVideoPath { get; set; }

    public string? RecipeCardImgPath { get; set; }

    public decimal? RecipeCookingTimeMinutes { get; set; }

    public decimal? RecipePreparingTimeMinutes { get; set; }

    public decimal? RecipeServings { get; set; }

    public string? RecipeDescription { get; set; }

    public string RecipeExplanation { get; set; } = null!;

    public decimal? ChiefId { get; set; }

    public string? RecipeStatus { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public DateTime? DeletedAt { get; set; }
}
