namespace Domain.Models.Views;

public class DocumentViewModel
{
    public Guid Id { get; set; }

    public string Code { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? SendingMethod { get; set; }

    public bool IsImportant { get; set; }

    public string? Content { get; set; }

    public Guid? DocumentTypeId { get; set; }
    
    public Guid OrganizationId { get; set; }

    public Guid StatusId { get; set; }

    public Guid SenderId { get; set; }

    public Guid ReceiverId { get; set; }

    public DateTime? DueDate { get; set; }

    public bool IsInternal { get; set; }

    public bool IsArchived { get; set; }

    public DateTime CreatedAt { get; set; }
}