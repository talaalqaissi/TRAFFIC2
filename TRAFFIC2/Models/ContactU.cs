using System;
using System.Collections.Generic;

namespace TRAFFIC2.Models;

public partial class ContactU
{
    public decimal ContactId { get; set; }

    public string? Firstname { get; set; }

    public string? Address { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Email { get; set; }

    public virtual ICollection<HomePage> HomePages { get; set; } = new List<HomePage>();
}
