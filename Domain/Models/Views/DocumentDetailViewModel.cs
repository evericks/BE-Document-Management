namespace Domain.Models.Views;

public class DocumentDetailViewModel
{
    public Guid Id { get; set; }

    public string Code { get; set; } = null!;

    public OrganizationViewModel Organization { get; set; } = null!;

    public string Name { get; set; } = null!;

    public bool IsImportant { get; set; }

    public string? Content { get; set; }

    public string? SendingMethod { get; set; }
    
    public DateTime? DueDate { get; set; }

    public bool IsInternal { get; set; }

    public bool IsArchived { get; set; }

    public DateTime CreatedAt { get; set; }

    public ICollection<AdditionalInformationDetailViewModel> AdditionalInformationDetails { get; set; } = new List<AdditionalInformationDetailViewModel>();
    
    public ICollection<AttachmentViewModel> Attachments { get; set; } = new List<AttachmentViewModel>();

    public ICollection<DocumentProcessDetailViewModel> DocumentProcesses { get; set; } = new List<DocumentProcessDetailViewModel>();

    public DocumentTypeDetailViewModel? DocumentType { get; set; }

    public UserViewModel Receiver { get; set; } = null!;

    public UserViewModel Sender { get; set; } = null!;

    public ICollection<DocumentLogViewModel> DocumentLogs { get; set; } = new List<DocumentLogViewModel>();

    public DocumentStatusViewModel Status { get; set; } = null!;
}