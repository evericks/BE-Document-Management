using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class AdditionalInformation
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public Guid DocumentTypeId { get; set; }

    public DateTime CreatedAt { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<AdditionalInformationDetail> AdditionalInformationDetails { get; set; } = new List<AdditionalInformationDetail>();

    public virtual DocumentType DocumentType { get; set; } = null!;
}
