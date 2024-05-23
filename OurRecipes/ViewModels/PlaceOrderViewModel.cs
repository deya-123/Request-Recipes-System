using System.ComponentModel.DataAnnotations;

namespace OurRecipes.ViewModels
{
    public class PlaceOrderViewModel
    {

        [Required(ErrorMessage = "Recipe ID is required.")]
        public decimal? RecipeId { get; set; }

        [Required(ErrorMessage = "Card number is required.")]
      //  [CreditCard(ErrorMessage = "Invalid card number.")]
        public string? CardNumber { get; set; }

        [Required(ErrorMessage = "Card expiry date is required.")]
        [RegularExpression(@"^(0[1-9]|1[0-2])\/?([0-9]{4}|[0-9]{2})$", ErrorMessage = "Invalid expiry date format. Expected format: MM/YY or MM/YYYY.")]
        public string? CardExpireDate { get; set; }

        [Required(ErrorMessage = "Card CVV is required.")]
        [RegularExpression(@"^\d{3,4}$", ErrorMessage = "Invalid CVV. It should be a 3 or 4 digit number.")]
        public string? CardCvv { get; set; }

        [Required(ErrorMessage = "Card holder name is required.")]
        [StringLength(100, ErrorMessage = "Card holder name cannot exceed 100 characters.")]
        public string? CardHolderName { get; set; }
    }
}
