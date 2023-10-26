using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace TRAFFIC2.Models;

public partial class Testimonial
{
    public decimal TestimonialId { get; set; }

    public decimal? UserId { get; set; }

    public string? TestimonialText { get; set; }

    [NotMapped]

    public bool IsAccepted { get; set; }
    public string Status { get; set;  }
   

    public virtual ICollection<HomePage> HomePages { get; set; } = new List<HomePage>();

    public virtual Userinformation? User { get; set; }
}
