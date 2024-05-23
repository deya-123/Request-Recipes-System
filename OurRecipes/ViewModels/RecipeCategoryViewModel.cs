using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace OurRecipes.ViewModels
{
    public class RecipeCategoryViewModel
    {

        public decimal CategoryId { get; set; }
        [Required(ErrorMessage = "Category Name is required")]
        [Display(Name = "Category Name")]
        public string? CategoryName { get; set; }
        [Required(ErrorMessage = "Category Type Id is required")]
        [Display(Name = "Category Type Id")]
        public decimal? CategoryTypeId { get; set; }
    }
}
