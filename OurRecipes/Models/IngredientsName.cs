using System;
using System.Collections.Generic;

namespace OurRecipes.Models;

public partial class IngredientsName
{
    public decimal IngredientsNamesId { get; set; }

    public decimal? IngredientNameId { get; set; }

    public decimal? IngredientId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public DateTime? DeletedAt { get; set; }
}
