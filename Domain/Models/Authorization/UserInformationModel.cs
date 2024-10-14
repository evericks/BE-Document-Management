namespace Domain.Models.Authorization;

public class UserInformationModel
{
    public Guid Id { get; set; }
    
    public string Username { get; set; } = null!;
    
    
    public string? AvatarUrl { get; set; }
    
    public string Name { get; set; } = null!;
    
    public string Role { get; set; } = null!;
    public string Department { get; set; } = null!;
    
    public string Status { get; set; } = null!;

}