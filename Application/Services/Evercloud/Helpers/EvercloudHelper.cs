namespace Application.Services.Evercloud.Helpers
{
    public static class EvercloudHelper
    {
        public static string BuildUrl(string endpoint, string bucket, string path)
        {
            return endpoint + "/" + bucket + "/" + path;
        }
    }
}
