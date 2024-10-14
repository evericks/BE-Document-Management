using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class DocumentDistribution
{
    public Guid Id { get; set; }

    public Guid DocumentId { get; set; }

    public Guid ReceiverId { get; set; }

    public bool? IsRead { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Document Document { get; set; } = null!;

    public virtual User Receiver { get; set; } = null!;
}
