using Microsoft.AspNetCore.Http;

namespace Application.Services.Evercloud.Models;

public class UploadCreateModel
{
    public IFormFile File { get; set; } = null!;
}