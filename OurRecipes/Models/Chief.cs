using System;
using System.Collections.Generic;

namespace OurRecipes.Models;

public partial class Chief
{
    public decimal ChiefId { get; set; }

    public string? ChiefExperiencePathFile { get; set; }

    public string? ChiefStatus { get; set; }

    public decimal? UserId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public virtual ICollection<Recipe> Recipes { get; set; } = new List<Recipe>();

    public virtual User? User { get; set; }
}
