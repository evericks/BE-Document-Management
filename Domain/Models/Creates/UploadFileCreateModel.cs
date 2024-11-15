using Microsoft.AspNetCore.Http;

namespace Domain.Models.Creates;

public class UploadFileCreateModel
{
    public IFormFile File { get; set; } = null!;
}