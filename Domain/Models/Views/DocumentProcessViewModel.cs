namespace Domain.Models.Views;

public class DocumentProcessDetailViewModel
{
    public Guid Id { get; set; }

    public string Status { get; set; } = null!;

    public DateTime ProcessedAt { get; set; }

    public string? Note { get; set; }

    public DateTime CreatedAt { get; set; }

    public UserViewModel ProcessBy { get; set; } = null!;

    public ProcessStepViewModel ProcessStep { get; set; } = null!;
}