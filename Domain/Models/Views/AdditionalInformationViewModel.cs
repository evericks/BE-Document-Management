namespace Domain.Models.Views;

public class AdditionalInformationViewModel
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;
    
    public string Description { get; set; } = null!;

    public Guid DocumentTypeId { get; set; }

    public DateTime CreatedAt { get; set; }
}