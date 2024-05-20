using System;
using System.Collections.Generic;

namespace Amazon.Payment.Sql2.Models;

public partial class Item
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public decimal Price { get; set; }

    public bool IsAvailable { get; set; }
}
