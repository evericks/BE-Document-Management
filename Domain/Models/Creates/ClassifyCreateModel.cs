namespace Domain.Models.Creates;

public class ClassifyCreateModel
{
    public Guid DocumentTypeId { get; set; }
    public bool IsImportant { get; set; }
    public bool IsInternal { get; set; }
    public bool IsArchived { get; set; }
    public List<AdditionalInformationDetailCreateModel> AdditionalInformations { get; set; }
}