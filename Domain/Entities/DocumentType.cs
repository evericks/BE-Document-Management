using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class DocumentType
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public Guid? ProcessId { get; set; }

    public virtual ICollection<Document> Documents { get; set; } = new List<Document>();

    public virtual Process? Process { get; set; }
}
