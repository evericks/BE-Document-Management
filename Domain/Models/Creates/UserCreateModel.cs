namespace Domain.Models.Creates;

public class UserCreateModel
{
    public string Username { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Phone { get; set; }
    
    public string? Gender { get; set; }
    
    public DateTime? Birthdate { get; set; }

    public string? Address { get; set; }

    public Guid RoleId { get; set; }
    
    public Guid DepartmentId { get; set; }
}