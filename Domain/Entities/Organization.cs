using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Organization
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public string Character { get; set; } = null!;

    public virtual ICollection<Document> DocumentOrganizations { get; set; } = new List<Document>();

    public virtual ICollection<Document> DocumentReceivingAgencies { get; set; } = new List<Document>();
}
