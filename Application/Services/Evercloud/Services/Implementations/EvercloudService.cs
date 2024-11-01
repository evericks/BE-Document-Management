using System.Net.Http.Headers;
using Application.Services.Evercloud.Helpers;
using Application.Services.Evercloud.Models;
using Application.Services.Evercloud.Services.Interfaces;
using Application.Settings;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Application.Services.Evercloud.Services.Implementations
{
    public class EvercloudService : IEvercloudService
    {
        private readonly HttpClient _httpClient;
        private readonly AppSettings _appSettings;

        public EvercloudService(HttpClient httpClient, IOptions<AppSettings> appSettings)
        {
            _httpClient = httpClient;
            _appSettings = appSettings.Value;
        }

        public async Task<UploadViewModel?> UploadAsync(IFormFile file, string path)
        {
            using var content = new MultipartFormDataContent();
            await using var fileStream = file.OpenReadStream();
            var fileContent = new StreamContent(fileStream);
            fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse(file.ContentType);
            content.Add(fileContent, "file", file.FileName);
            _httpClient.DefaultRequestHeaders.Add("Secret", _appSettings.Secret);
            var url = EvercloudHelper.BuildUrl(_appSettings.EvercloudUrl, _appSettings.EvercloudBucket, path);
            var response = await _httpClient.PostAsync(url, content);
            var result = await response.Content.ReadAsStringAsync();
            var upload = JsonConvert.DeserializeObject<UploadViewModel>(result);
            upload.FileName = file.FileName;
            upload.FileExtension = Path.GetExtension(file.FileName);
            return upload;
        }
    }
}