namespace Domain.Models.Views;

public class DocumentTypeViewModel
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;
    
    public Guid ProcessId { get; set; }

    public DateTime CreatedAt { get; set; }
    
    public ICollection<AdditionalInformationViewModel> AdditionalInformations { get; set; } = new List<AdditionalInformationViewModel>();
}