using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace OurRecipes.ViewModels
{
    public class ContactInfoViewModel
    {
        public decimal ContactInfoId { get; set; }

        public string? LocationOnMap { get; set; }
        [Required(ErrorMessage = "Address is required")]
        public string? Address { get; set; }
        [Required(ErrorMessage = "Phone is required")]
        public string? Phone { get; set; }
        [Required(ErrorMessage = "Email  is required")]
        public string? Email { get; set; }
    }
}
