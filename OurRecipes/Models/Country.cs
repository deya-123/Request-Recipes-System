using System;
using System.Collections.Generic;

namespace OurRecipes.Models;

public partial class Country
{
    public decimal CountryId { get; set; }

    public string? CountryName { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
