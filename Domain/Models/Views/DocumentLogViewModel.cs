namespace Domain.Models.Views;

public class DocumentLogViewModel
{
    public Guid Id { get; set; }

    public UserViewModel User { get; set; }

    public string Action { get; set; } = null!;

    public string? Note { get; set; }

    public DateTime CreatedAt { get; set; }
}