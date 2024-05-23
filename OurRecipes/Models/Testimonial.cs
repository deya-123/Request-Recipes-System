using System;
using System.Collections.Generic;

namespace OurRecipes.Models;

public partial class Testimonial
{
    public decimal TestimonialId { get; set; }

    public string? TestimonialText { get; set; }

    public decimal? UserId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public string? TestimonialStatus { get; set; }

    public virtual User? User { get; set; }
}
