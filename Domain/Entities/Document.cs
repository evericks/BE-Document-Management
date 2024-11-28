using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Document
{
    public Guid Id { get; set; }

    public string Code { get; set; } = null!;

    public string Name { get; set; } = null!;

    public bool IsImportant { get; set; }

    public string? Content { get; set; }

    public Guid? DocumentTypeId { get; set; }

    public Guid StatusId { get; set; }

    public Guid SenderId { get; set; }

    public Guid ReceiverId { get; set; }

    public DateTime? DueDate { get; set; }

    public bool IsInternal { get; set; }

    public bool IsArchived { get; set; }

    public DateTime CreatedAt { get; set; }

    public Guid OrganizationId { get; set; }

    public Guid? ReceivingAgencyId { get; set; }

    public string? SendingMethod { get; set; }

    public virtual ICollection<AdditionalInformationDetail> AdditionalInformationDetails { get; set; } = new List<AdditionalInformationDetail>();

    public virtual ICollection<Archive> Archives { get; set; } = new List<Archive>();

    public virtual ICollection<Attachment> Attachments { get; set; } = new List<Attachment>();

    public virtual ICollection<DocumentDistribution> DocumentDistributions { get; set; } = new List<DocumentDistribution>();

    public virtual ICollection<DocumentLog> DocumentLogs { get; set; } = new List<DocumentLog>();

    public virtual ICollection<DocumentProcess> DocumentProcesses { get; set; } = new List<DocumentProcess>();

    public virtual DocumentType? DocumentType { get; set; }

    public virtual Organization Organization { get; set; } = null!;

    public virtual User Receiver { get; set; } = null!;

    public virtual Organization? ReceivingAgency { get; set; }

    public virtual User Sender { get; set; } = null!;

    public virtual DocumentStatus Status { get; set; } = null!;
}
