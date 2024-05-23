using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OurRecipes.ViewModels
{
    public class RecipeViewModel
    {
        public decimal RecipeId { get; set; }
        [Required(ErrorMessage = "Recipe name is required.")]
        public string? RecipeName { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Recipe price must be a positive number.")]
        public decimal? RecipePrice { get; set; }

        public decimal? RecipeCategoryId { get; set; }
        //public string? RecipeMainImgPath { get; set; }

     
        //public string? RecipeVideoPath { get; set; }

        //public string? RecipeCardImgPath { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Cooking time must be a positive number.")]
        public decimal? RecipeCookingTimeMinutes { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Preparing time must be a positive number.")]
        public decimal? RecipePreparingTimeMinutes { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Number of servings must be a positive number.")]
        public decimal? RecipeServings { get; set; }

        [Required(ErrorMessage = "Recipe description is required.")]
        public string? RecipeDescription { get; set; }

        [Required(ErrorMessage = "Recipe explanation is required.")]
        public string? RecipeExplanation { get; set; } = null!;

        public decimal? ChiefId { get; set; }
        public string? RecipeStatus { get; set; }
        public DateTime? CreatedAt { get; set; }

        [NotMapped]
        public IFormFile? RecipeMainImage { get; set; }
        [NotMapped]
        public IFormFile? RecipeCardImage{ get; set; }
        [NotMapped]
        public IFormFile? RecipeVideo { get; set; }


    }
}
