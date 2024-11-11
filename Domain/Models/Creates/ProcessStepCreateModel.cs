namespace Domain.Models.Creates;

public class ProcessStepCreateModel
{
    public int StepNumber { get; set; }

    public string Name { get; set; } = null!;
    public Guid? RoleId { get; set; } = null!;

    public string? Description { get; set; }
}