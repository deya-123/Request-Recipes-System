using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace OurRecipes.ViewModels
{
    public class HomeViewModel
    {
        public decimal HomeId { get; set; }
  
        [Required(ErrorMessage = "Home Website Name  is required")]
        public string? HomeWebsiteName { get; set; }
        [Required(ErrorMessage = "Home Title is required")]
        public string? HomeTitle { get; set; }

        public string? WorkingDays { get; set; }

        public string? FacbookLink { get; set; }

        public string? InsLink { get; set; }

        public string? YoutubeLink { get; set; }

        public string? HomeDesc { get; set; }

      

        [NotMapped]
        public IFormFile? HomeImageFile { get; set; }

        [NotMapped]
        public IFormFile? HomeLogoFile { get; set; }

    }
}
