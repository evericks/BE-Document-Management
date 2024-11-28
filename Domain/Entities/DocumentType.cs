using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class DocumentType
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public Guid? ProcessId { get; set; }

    public string Character { get; set; } = null!;

    public virtual ICollection<AdditionalInformation> AdditionalInformations { get; set; } = new List<AdditionalInformation>();

    public virtual ICollection<Document> Documents { get; set; } = new List<Document>();

    public virtual Process? Process { get; set; }
}
