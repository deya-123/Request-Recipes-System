using System.ComponentModel.DataAnnotations;

namespace OurRecipes.ViewModels
{
    public class TestimonialViewModel
    {
        public decimal TestimonialId { get; set; }

        [Required(ErrorMessage = "Testimonial Text is required")]
        [Display(Name = "Testimonial Text")]
        public string? TestimonialText { get; set; }

        public string? TestimonialStatus { get; set; }
        public decimal? UserId { get; set; }

        public DateTime? CreatedAt { get; set; }
    }
}
