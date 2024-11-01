namespace Domain.Models.Views;

public class AttachmentViewModel
{
    public Guid Id { get; set; }

    public string? Name { get; set; }

    public string Url { get; set; } = null!;

    public string MediaType { get; set; } = null!;

    public Guid DocumentId { get; set; }

    public DateTime CreatedAt { get; set; }
}