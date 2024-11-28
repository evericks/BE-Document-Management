using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class AdditionalInformationDetail
{
    public Guid Id { get; set; }

    public string Value { get; set; } = null!;

    public Guid AdditionalInformationId { get; set; }

    public Guid DocumentId { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual AdditionalInformation AdditionalInformation { get; set; } = null!;

    public virtual Document Document { get; set; } = null!;
}
