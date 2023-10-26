using System;
using System.Collections.Generic;

namespace TRAFFIC2.Models;

public partial class Payment
{
    public decimal PaymentId { get; set; }

    public decimal? UserId { get; set; }

    public decimal? ViolationId { get; set; }

    public string? PaymentMethod { get; set; }

    public string? TransactionId { get; set; }

    public DateTime? PaymentDate { get; set; }

    public decimal? AmountPaid { get; set; }

    public string? PaymentStatus { get; set; }

    public virtual Userinformation? User { get; set; }

    public virtual Violation? Violation { get; set; }
}
