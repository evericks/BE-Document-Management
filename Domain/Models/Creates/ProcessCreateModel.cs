namespace Domain.Models.Creates;

public class ProcessCreateModel
{
    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public ICollection<ProcessStepCreateModel> ProcessSteps { get; set; } = new List<ProcessStepCreateModel>();
}