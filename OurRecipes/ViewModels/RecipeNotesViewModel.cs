using OurRecipes.Models;

namespace OurRecipes.ViewModels
{
    public class RecipeNotesViewModel
    {
        public decimal RecipeNoteId { get; set; }

        public string? RecipeNoteTitle { get; set; }

        public string? RecipeNoteDescription { get; set; }

        public decimal? RecipeId { get; set; }

        public DateTime? CreatedAt { get; set; }


    }
}
