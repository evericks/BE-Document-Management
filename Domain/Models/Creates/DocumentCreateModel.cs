using Microsoft.AspNetCore.Http;

namespace Domain.Models.Creates;

public class DocumentCreateModel
{
    public Guid OrganizationId { get; set; }
    
    public Guid ReceivingAgencyId { get; set; }

    public string Name { get; set; } = null!;

    public bool IsImportant { get; set; }

    public string? Content { get; set; }

    public Guid? StatusId { get; set; }

    public Guid? ReceiverId { get; set; }

    public DateTime? DueDate { get; set; }

    public bool IsInternal { get; set; }

    public bool IsArchived { get; set; }
    public ICollection<IFormFile>? Attachments { get; set; }
}