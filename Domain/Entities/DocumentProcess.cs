using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class DocumentProcess
{
    public Guid Id { get; set; }

    public Guid DocumentId { get; set; }

    public Guid ProcessStepId { get; set; }

    public string Status { get; set; } = null!;

    public Guid ProcessById { get; set; }

    public DateTime ProcessedAt { get; set; }

    public string? Note { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Document Document { get; set; } = null!;

    public virtual User ProcessBy { get; set; } = null!;

    public virtual ProcessStep ProcessStep { get; set; } = null!;
}
