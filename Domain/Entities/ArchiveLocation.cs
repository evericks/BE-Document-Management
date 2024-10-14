using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class ArchiveLocation
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<Archive> Archives { get; set; } = new List<Archive>();
}
