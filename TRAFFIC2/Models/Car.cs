using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace TRAFFIC2.Models;

public partial class Car
{
    public decimal CarId { get; set; }

    public decimal? UserId { get; set; }

    public string? Carname { get; set; }

    public string? Model { get; set; }

    public decimal? Year { get; set; }

    public string? Color { get; set; }

    public string? LicensePlate { get; set; }

    [NotMapped]
    [NotNull]
    public DateTime? AppointmentDate { get; set; }


    public virtual Userinformation? User { get; set; }

    public virtual ICollection<Violation> Violations { get; set; } = new List<Violation>();

    public virtual ICollection<Violationsinquiry> Violationsinquiries { get; set; } = new List<Violationsinquiry>();
}
