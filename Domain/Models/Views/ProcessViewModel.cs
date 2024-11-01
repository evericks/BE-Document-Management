namespace Domain.Models.Views;

public class ProcessViewModel
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public Guid DocumentTypeId { get; set; }
    public ICollection<ProcessStepViewModel> ProcessSteps { get; set; } = new List<ProcessStepViewModel>();

    public DateTime CreatedAt { get; set; }
}