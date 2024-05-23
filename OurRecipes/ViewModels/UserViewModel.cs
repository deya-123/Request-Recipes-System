using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OurRecipes.ViewModels
{
    public class UserViewModel
    {
        public decimal UserId { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string? UserEmail { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters long")]
        public string? UserPassword { get; set; }

        [Required(ErrorMessage = "Full name is required")]
        [Display(Name = "Full Name")]
        public string? UserName { get; set; }

        [Required(ErrorMessage = "Role is required")]
        [Display(Name = "Role")]
        public decimal? RoleId { get; set; }
        [Required(ErrorMessage = "Please enter a phone number.")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Invalid phone number format. Please enter digits only.")]
        public string? UserPhone { get; set; }

        [Required(ErrorMessage = "Please select a country.")]
        [Range(1, int.MaxValue, ErrorMessage = "Invalid country ID. Please select a valid country.")]
        public decimal? UserCountryId { get; set; }

        [Required(ErrorMessage = "Please select a gender.")]
        public string? UserGender { get; set; }

      

    }
}
