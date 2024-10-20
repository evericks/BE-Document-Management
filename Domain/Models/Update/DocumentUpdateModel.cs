namespace Domain.Models.Update;

public class DocumentUpdateModel
{
    public string IssuingAgency { get; set; } = null!;

    public string Name { get; set; } = null!;

    public bool IsImportant { get; set; }

    public string? Content { get; set; }

    public Guid DocumentTypeId { get; set; }

    public Guid StatusId { get; set; }

    public Guid ReceiverId { get; set; }

    public DateTime? DueDate { get; set; }

    public bool IsInternal { get; set; }

    public bool IsArchived { get; set; }

    public DateTime CreatedAt { get; set; }
}