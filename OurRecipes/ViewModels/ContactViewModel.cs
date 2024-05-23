using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace OurRecipes.ViewModels
{
    public class ContactViewModel
    {
        public decimal ContactId { get; set; }

        [Required(ErrorMessage = "Contact Message is required")]
        [Display(Name = "Contact Message")]
        public string? ContactMessage { get; set; }
        [Required(ErrorMessage = "Contact Sender Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string? ContactSenderEmail { get; set; }
        [Required(ErrorMessage = "Contact Sender Name is required")]
        [Display(Name = "Contact Sender Name")]
        public string? ContactSenderName { get; set; }
    }
}
