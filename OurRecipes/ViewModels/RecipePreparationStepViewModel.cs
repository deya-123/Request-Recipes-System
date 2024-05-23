using OurRecipes.Models;

namespace OurRecipes.ViewModels
{
    public class RecipePreparationStepViewModel
    {

        public decimal RecipePreparationStepId { get; set; }

        public string? RecipePreparationStepDescription { get; set; }

        public decimal? RecipeId { get; set; }

        public DateTime? CreatedAt { get; set; }



    }
}
