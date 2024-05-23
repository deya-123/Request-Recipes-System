using System;
using System.Collections.Generic;

namespace OurRecipes.Models;

public partial class About
{
    public decimal AboutId { get; set; }

    public string? AboutTitle { get; set; }

    public string? AboutBody { get; set; }

    public string? AboutImage { get; set; }
}
