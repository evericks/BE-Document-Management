namespace Application.Services.Evercloud.Models
{
    public class UploadViewModel
    {
        public Guid Id { get; set; }
        public string FileName { get; set; } = null!;
        public string? Url { get; set; } = null!;
        public string FileExtension { get; set; } = null!;
    }
}
