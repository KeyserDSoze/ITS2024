using System;
using System.Collections.Generic;

namespace Amazon.Payment.Sql2.AuthModels;

public partial class User
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public virtual ICollection<Role> Roles { get; set; } = new List<Role>();
}
