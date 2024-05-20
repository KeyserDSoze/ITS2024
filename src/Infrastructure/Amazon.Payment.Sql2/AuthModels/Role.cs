using System;
using System.Collections.Generic;

namespace Amazon.Payment.Sql2.AuthModels;

public partial class Role
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
