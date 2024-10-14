using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Attachment
{
    public Guid Id { get; set; }

    public string? Name { get; set; }

    public string Url { get; set; } = null!;

    public string MediaType { get; set; } = null!;

    public Guid DocumentId { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Document Document { get; set; } = null!;
}
