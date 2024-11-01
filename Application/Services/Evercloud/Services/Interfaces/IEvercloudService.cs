using Application.Services.Evercloud.Models;
using Microsoft.AspNetCore.Http;

namespace Application.Services.Evercloud.Services.Interfaces
{
    public interface IEvercloudService
    {
        Task<UploadViewModel?> UploadAsync(IFormFile file, string path);
    }
}
