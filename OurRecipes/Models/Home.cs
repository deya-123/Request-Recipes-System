using System;
using System.Collections.Generic;

namespace OurRecipes.Models;

public partial class Home
{
    public decimal HomeId { get; set; }

    public string? HomeImage { get; set; }

    public string? HomeWebsiteName { get; set; }

    public string? HomeTitle { get; set; }

    public string? WorkingDays { get; set; }

    public string? FacbookLink { get; set; }

    public string? InsLink { get; set; }

    public string? YoutubeLink { get; set; }

    public string? HomeDesc { get; set; }

    public string? HomeLogo { get; set; }
}
