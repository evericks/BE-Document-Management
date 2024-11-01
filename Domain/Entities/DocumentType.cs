using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class DocumentType
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<Document> Documents { get; set; } = new List<Document>();

    public virtual ICollection<Process> Processes { get; set; } = new List<Process>();
}
