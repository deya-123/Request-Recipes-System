using System;
using System.Collections.Generic;

namespace OurRecipes.Models;

public partial class User
{
    public decimal UserId { get; set; }

    public string? UserEmail { get; set; }

    public string? UserPassword { get; set; }

    public string? UserPhone { get; set; }

    public decimal? UserCountryId { get; set; }

    public string? UserGender { get; set; }

    public decimal? RoleId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public string? EmailVerificationToken { get; set; }

    public bool? IsEmailVerification { get; set; }

    public DateTime? EmailVerificationTokenExpireDate { get; set; }

    public string? PasswordVerificationToken { get; set; }

    public DateTime? PasswordVerificationTokenExpireDate { get; set; }

    public string? UserName { get; set; }

    public string? UserImage { get; set; }

    public virtual ICollection<Chief> Chiefs { get; set; } = new List<Chief>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual Role? Role { get; set; }

    public virtual ICollection<Testimonial> Testimonials { get; set; } = new List<Testimonial>();

    public virtual Country? UserCountry { get; set; }
}
