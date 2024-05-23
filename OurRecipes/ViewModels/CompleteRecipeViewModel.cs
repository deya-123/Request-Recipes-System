namespace OurRecipes.ViewModels
{
    public class CompleteRecipeViewModel
    {

        public RecipeNotesViewModel RecipeNotesViewModel { get; set; }
        public RecipeViewModel RecipeViewModel{ get; set; }
        public RecipePreparationStepViewModel RecipePreparationStepViewModel{ get; set; }

        public IngredientViewModel IngredientViewModel{ get; set; }
        public UpdateStatusViewModel UpdateStatusViewModel { get; set; }
    }
}
