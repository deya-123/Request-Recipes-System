using OurRecipes.Models;

namespace OurRecipes.ViewModels
{
    public class IngredientViewModel
    {
        public decimal IngredientId { get; set; }

        public string? IngredientCustomName { get; set; }

        public string? IngredientQuantity { get; set; }

        public decimal? IngredientUnitId { get; set; }

        public decimal? RecipeId { get; set; }

        public DateTime? CreatedAt { get; set; }

    }
}
