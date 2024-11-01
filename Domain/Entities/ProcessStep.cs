using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class ProcessStep
{
    public Guid Id { get; set; }

    public Guid ProcessId { get; set; }

    public int StepNumber { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public Guid RoleId { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<DocumentProcess> DocumentProcesses { get; set; } = new List<DocumentProcess>();

    public virtual Process Process { get; set; } = null!;

    public virtual Role Role { get; set; } = null!;
}
