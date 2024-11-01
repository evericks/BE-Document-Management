namespace Application.Settings
{
    public class AppSettings
    {
        public required string Secret { get; set; }
        public string EvercloudUrl { get; set; } = null!;
        public string EvercloudBucket { get; set; } = null!;
    }
}
