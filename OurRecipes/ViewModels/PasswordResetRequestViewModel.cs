using System.ComponentModel.DataAnnotations;

namespace OurRecipes.ViewModels
{
    public class PasswordResetRequestViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
