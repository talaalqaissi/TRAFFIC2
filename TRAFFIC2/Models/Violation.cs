using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace TRAFFIC2.Models;

public partial class Violation
{
    public decimal ViolationId { get; set; }

    public decimal? UserId { get; set; }

    public decimal? CarId { get; set; }

    public string? Description { get; set; }

    public string? Location { get; set; }
    [NotMapped]
    [NotNull]
    public int ViolationsPrice { get; set; }
    public DateTime? ViolationDate { get; set; }

    public string? AdditionalNotes { get; set; }

    public virtual Car? Car { get; set; }

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual Userinformation? User { get; set; }
    //public bool IsPaid { get; internal set; }
}
