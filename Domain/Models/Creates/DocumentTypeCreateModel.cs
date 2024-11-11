namespace Domain.Models.Creates;

public class DocumentTypeCreateModel
{
    public string Name { get; set; } = null!;
    public Guid ProcessId { get; set; }
}