namespace Domain.Models.Views;

public class DocumentStatusViewModel
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public DateTime CreatedAt { get; set; }
}