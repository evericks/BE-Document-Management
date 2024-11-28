namespace Domain.Models.Views;

public class AdditionalInformationDetailViewModel
{
    public Guid Id { get; set; }

    public string Value { get; set; } = null!;

    public AdditionalInformationViewModel AdditionalInformation { get; set; }

    public Guid DocumentId { get; set; }

    public DateTime CreatedAt { get; set; }
}