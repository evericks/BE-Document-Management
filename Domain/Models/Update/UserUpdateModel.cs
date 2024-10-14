namespace Domain.Models.Update;

public class UserUpdateModel
{
    public Guid Id { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Phone { get; set; }

    public string? Address { get; set; }

    public string Status { get; set; } = null!;

    public Guid RoleId { get; set; }

    public DateTime CreateAt { get; set; }
}