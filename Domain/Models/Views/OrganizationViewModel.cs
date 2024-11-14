namespace Domain.Models.Views;

public class OrganizationViewModel
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public DateTime CreatedAt { get; set; }
}