using Domain.Models.Creates;

namespace Domain.Models.Update;

public class DocumentTypeUpdateModel
{
    public string? Name { get; set; }
    public Guid? ProcessId { get; set; }
    public ICollection<AdditionalInformationCreateModel>? AdditionalInformations { get; set; }
}