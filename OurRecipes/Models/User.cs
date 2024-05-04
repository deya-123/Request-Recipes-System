using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OurRecipes.Models;

public partial class User
{
    public decimal UserId { get; set; }
    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid Email Address.")]
    [MaxLength(256, ErrorMessage = "Email cannot be longer than 256 characters.")]

    public string? UserEmail { get; set; }
    [Required(ErrorMessage = "Password is required.")]
    [StringLength(100, ErrorMessage = "The password must be at least {2} characters long.", MinimumLength = 8)]
    [DataType(DataType.Password)]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,100}$", ErrorMessage = "Passwords must be at least 8 characters and contain at least one uppercase letter, one lowercase letter, and one number.")]

    public string? UserPassword { get; set; }

    public string? UserPhone { get; set; }

    public decimal? UserCountryId { get; set; }

    public string? UserGender { get; set; }

    public decimal? RoleId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public DateTime? DeletedAt { get; set; }
}
