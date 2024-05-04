using System;
using System.Collections.Generic;

namespace OurRecipes.Models;

public partial class Contact
{
    public decimal ContactId { get; set; }

    public string? ContactMessage { get; set; }

    public string? ContactSenderEmail { get; set; }

    public string? ContactSenderName { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public DateTime? DeletedAt { get; set; }
}
