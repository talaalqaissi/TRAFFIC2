using System;
using System.Collections.Generic;

namespace TRAFFIC2.Models;

public partial class HomePage
{
    public decimal HomeId { get; set; }

    public decimal? ImageId { get; set; }

    public string? Title { get; set; }

    public string? Content { get; set; }

    public decimal? AboutId { get; set; }

    public decimal? ContactId { get; set; }

    public decimal? TestimonialId { get; set; }

    public virtual AboutU? About { get; set; }

    public virtual ContactU? Contact { get; set; }

    public virtual Image? Image { get; set; }

    public virtual Testimonial? Testimonial { get; set; }
}
