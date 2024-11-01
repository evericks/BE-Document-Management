namespace Domain.Models.Views;

public class UserViewModel
{
    public Guid Id { get; set; }

    public string Username { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Phone { get; set; }

    public string? Address { get; set; }

    public string Status { get; set; } = null!;
    
    public DateTime? Birthdate { get; set; }
    
    public string? Gender { get; set; }

    public Guid RoleId { get; set; }
    
    public Guid DepartmentId { get; set; }

    public DateTime CreatedAt { get; set; }
}