namespace Domain.Models.Views;

public class ProcessStepViewModel
{
    public Guid Id { get; set; }

    public int StepNumber { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public Guid RoleId { get; set; }

    public DateTime CreatedAt { get; set; }
}