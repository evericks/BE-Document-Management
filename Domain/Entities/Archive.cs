using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Archive
{
    public Guid Id { get; set; }

    public Guid DocumentId { get; set; }

    public Guid ArchivedById { get; set; }

    public Guid ArchiveLocationId { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual ArchiveLocation ArchiveLocation { get; set; } = null!;

    public virtual User ArchivedBy { get; set; } = null!;

    public virtual Document Document { get; set; } = null!;
}
