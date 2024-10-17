namespace Domain.Models.Update;

public class DepartmentUpdateModel
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public DateTime CreatedAt { get; set; }
}