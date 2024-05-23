using System;
using System.Collections.Generic;

namespace OurRecipes.Models;

public partial class RecipeNote
{
    public decimal RecipeNoteId { get; set; }

    public string? RecipeNoteTitle { get; set; }

    public string? RecipeNoteDescription { get; set; }

    public decimal? RecipeId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public virtual Recipe? Recipe { get; set; }
}
