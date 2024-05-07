using System.ComponentModel.DataAnnotations;

namespace OurRecipes.ViewModels
{
    public class ResetPasswordViewModel
    {
        
       
        [Required]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters long")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters long")]
        public string ConfirmPassword { get; set; }

        public string Token { get; set; }
    }
}
