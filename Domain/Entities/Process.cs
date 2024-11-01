using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Process
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public Guid DocumentTypeId { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual DocumentType DocumentType { get; set; } = null!;

    public virtual ICollection<ProcessStep> ProcessSteps { get; set; } = new List<ProcessStep>();
}
