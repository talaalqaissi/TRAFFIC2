using System;
using System.Collections.Generic;

namespace TRAFFIC2.Models;

public partial class AboutU
{
    public decimal AboutId { get; set; }

    public string? Title { get; set; }

    public string? Content { get; set; }

    public virtual ICollection<HomePage> HomePages { get; set; } = new List<HomePage>();
}
