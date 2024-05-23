using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace OurRecipes.ViewModels
{
    public class AboutViewModel
    {



        public decimal AboutId { get; set; }
        [Required(ErrorMessage = "About Title is required")]
        public string? AboutTitle { get; set; }
        [Required(ErrorMessage = "About Body is required")]
        public string? AboutBody { get; set; }


        [NotMapped]
        public IFormFile? AboutImageFile { get; set; }
    }
}
