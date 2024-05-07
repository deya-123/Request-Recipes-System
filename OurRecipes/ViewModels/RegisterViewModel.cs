using System.ComponentModel.DataAnnotations;

namespace OurRecipes.ViewModels
{
    public class RegisterViewModel
    {
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
        public int? RoleId{ get; set; }
    }
}
