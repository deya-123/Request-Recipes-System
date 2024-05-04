using System;
using System.Collections.Generic;

namespace OurRecipes.Models;

public partial class Invoice
{
    public decimal InvoiceId { get; set; }

    public string? InvoiceNumber { get; set; }

    public string? InvoicePaymentStatus { get; set; }

    public decimal? OrderId { get; set; }

    public decimal? InvoiceTotalAmount { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public DateTime? DeletedAt { get; set; }
}
