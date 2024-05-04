using System;
using System.Collections.Generic;

namespace OurRecipes.Models;

public partial class IngredientName
{
    public decimal IngredientNameId { get; set; }

    public string? IngredientName1 { get; set; }

    public string? IngredientType { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public DateTime? DeletedAt { get; set; }
}
