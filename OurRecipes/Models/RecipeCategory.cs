using System;
using System.Collections.Generic;

namespace OurRecipes.Models;

public partial class RecipeCategory
{
    public decimal CategoryId { get; set; }

    public string? CategoryName { get; set; }

    public string? CategoryType { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public DateTime? DeletedAt { get; set; }
}
