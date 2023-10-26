using System;
using System.Collections.Generic;

namespace TRAFFIC2.Models;

public partial class Userinformation
{
   
    public decimal UserId { get; set; }


    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Email { get; set; }

    public string? Address { get; set; }

    public DateTime? DateOfBirth { get; set; }

    public virtual ICollection<Car> Cars { get; set; } = new List<Car>();

    public virtual ICollection<Login> Logins { get; set; } = new List<Login>();

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual ICollection<Testimonial> Testimonials { get; set; } = new List<Testimonial>();

    public virtual ICollection<Violation> Violations { get; set; } = new List<Violation>();
}
