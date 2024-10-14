using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class User
{
    public Guid Id { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Gender { get; set; }

    public string? Phone { get; set; }

    public DateTime? Birthdate { get; set; }

    public string? AvatarUrl { get; set; }

    public string Status { get; set; } = null!;

    public Guid RoleId { get; set; }

    public Guid DepartmentId { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<Archive> Archives { get; set; } = new List<Archive>();

    public virtual Department Department { get; set; } = null!;

    public virtual ICollection<DocumentDistribution> DocumentDistributions { get; set; } = new List<DocumentDistribution>();

    public virtual ICollection<DocumentLog> DocumentLogs { get; set; } = new List<DocumentLog>();

    public virtual ICollection<Document> DocumentReceivers { get; set; } = new List<Document>();

    public virtual ICollection<Document> DocumentSenders { get; set; } = new List<Document>();

    public virtual Role Role { get; set; } = null!;
}
