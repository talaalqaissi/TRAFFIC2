using System;
using System.Collections.Generic;

namespace TRAFFIC2.Models;

public partial class Login
{
    public decimal LoginId { get; set; }

    public string? Username { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Password { get; set; }

    public decimal? RoleId { get; set; }

    public decimal? UserId { get; set; }

    public virtual Role? Role { get; set; }

    public virtual Userinformation? User { get; set; }
}
