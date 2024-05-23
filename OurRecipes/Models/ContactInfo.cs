using System;
using System.Collections.Generic;

namespace OurRecipes.Models;

public partial class ContactInfo
{
    public decimal ContactInfoId { get; set; }

    public string? LocationOnMap { get; set; }

    public string? Address { get; set; }

    public string? Phone { get; set; }

    public string? Email { get; set; }
}
