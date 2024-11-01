namespace Domain.Models.Views;

public class DocumentTypeDetailViewModel
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public ICollection<ProcessViewModel> Processes { get; set; } = new List<ProcessViewModel>();
}