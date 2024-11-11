using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Process
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<DocumentType> DocumentTypes { get; set; } = new List<DocumentType>();

    public virtual ICollection<ProcessStep> ProcessSteps { get; set; } = new List<ProcessStep>();
}
