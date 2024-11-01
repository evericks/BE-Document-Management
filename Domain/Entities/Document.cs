using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Document
{
    public Guid Id { get; set; }

    public string Code { get; set; } = null!;

    public string IssuingAgency { get; set; } = null!;

    public string Name { get; set; } = null!;

    public bool IsImportant { get; set; }

    public string? Content { get; set; }

    public Guid DocumentTypeId { get; set; }

    public Guid StatusId { get; set; }

    public Guid SenderId { get; set; }

    public Guid ReceiverId { get; set; }

    public DateTime? DueDate { get; set; }

    public bool IsInternal { get; set; }

    public bool IsArchived { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<Archive> Archives { get; set; } = new List<Archive>();

    public virtual ICollection<Attachment> Attachments { get; set; } = new List<Attachment>();

    public virtual ICollection<DocumentDistribution> DocumentDistributions { get; set; } = new List<DocumentDistribution>();

    public virtual ICollection<DocumentLog> DocumentLogs { get; set; } = new List<DocumentLog>();

    public virtual ICollection<DocumentProcess> DocumentProcesses { get; set; } = new List<DocumentProcess>();

    public virtual DocumentType DocumentType { get; set; } = null!;

    public virtual User Receiver { get; set; } = null!;

    public virtual User Sender { get; set; } = null!;

    public virtual DocumentStatus Status { get; set; } = null!;
}
