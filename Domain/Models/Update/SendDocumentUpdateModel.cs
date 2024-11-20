namespace Domain.Models.Update;

public class SendDocumentUpdateModel
{
    public Guid ReceiverId { get; set; }
    public string Message { get; set; } = null!;
}