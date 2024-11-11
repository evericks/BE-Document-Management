namespace Domain.Models.Views;

public class DocumentTypeDetailViewModel
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public ProcessViewModel Process { get; set; } = null!;
}