using System.Text;
using Application.Services.Cache.Interfaces;
using Application.Settings;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Attributes;

public class CacheAttribute : Attribute, IAsyncActionFilter
{
    private readonly int _timeToLiveSeconds;

    public CacheAttribute(int timeToLiveSeconds = 1000)
    {
        _timeToLiveSeconds = timeToLiveSeconds;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var redisSettings = context.HttpContext.RequestServices.GetRequiredService<RedisSettings>();

        if (!redisSettings.Enabled)
        {
            await next();
            return;
        }

        var cacheService = context.HttpContext.RequestServices.GetRequiredService<IResponseCacheService>();
        var cacheKey = await GenerateCacheKeyFromRequest(context.HttpContext);
        var cacheResponse = await cacheService.GetCacheResponseAsync(cacheKey);
        if (!string.IsNullOrEmpty(cacheResponse))
        {
            var contentResult = new ContentResult
            {
                Content = cacheResponse,
                ContentType = "application/json",
                StatusCode = 200
            };
            context.Result = contentResult;
            return;
        }

        var executedContext = await next();

        if (executedContext.Result is OkObjectResult { Value: not null } objectResult)
        {
            await cacheService.SetCacheResponseAsync(cacheKey, objectResult.Value,
                TimeSpan.FromSeconds(_timeToLiveSeconds));
        }
    }

    private async Task<string> GenerateCacheKeyFromRequest(HttpContext context)
    {
        var keyBuilder = new StringBuilder();
        await GetRequestBody(context);
        keyBuilder.Append($"{context.Request.Method}{context.Request.Path}");
        foreach (var (key, value) in context.Request.Query.OrderBy(x => x.Key))
        {
            keyBuilder.Append($"|{key}-{value}");
        }

        return keyBuilder.ToString();
    }

    private async Task<string> GetRequestBody(HttpContext context)
    {
        context.Request.EnableBuffering(); // Cho phép đọc nhiều lần từ request body

        using (var reader = new StreamReader(context.Request.Body, Encoding.UTF8, true, 1024, leaveOpen: true))
        {
            var body = await reader.ReadToEndAsync();
            context.Request.Body.Position = 0; // Đặt lại vị trí của stream sau khi đọc

            // Xử lý body JSON tại đây
            Console.WriteLine(body); // In ra console, hoặc xử lý theo cách bạn muốn
        }

        return "";
    }
}