using System;
using System.Collections.Generic;

namespace AdminPanel.Models;

public partial class User
{
    public long Id { get; set; }

    public string? Name { get; set; }

    public string? Password { get; set; }

    public string? Roles { get; set; }

    public string? Status { get; set; }

    public string? Username { get; set; }
}
