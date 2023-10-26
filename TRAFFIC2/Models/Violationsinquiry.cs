using System;
using System.Collections.Generic;

namespace TRAFFIC2.Models;

public partial class Violationsinquiry
{
    public decimal EnquiryId { get; set; }

    public decimal? CarId { get; set; }

    public virtual Car? Car { get; set; }
}
