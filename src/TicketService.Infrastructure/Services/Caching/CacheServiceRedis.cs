using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using TicketService.Application.Abstractions.Cache;

namespace TicketService.Infrastructure.Services.Caching;

public class CacheServiceRedis : ICacheService
{
    private readonly IDistributedCache _cache;
    private readonly ILogger<CacheServiceRedis> _logger;

    public CacheServiceRedis(
        IDistributedCache cache,
        ILogger<CacheServiceRedis> logger)
    {
        _cache = cache;
        _logger = logger;
    }
    
    public async Task<T?> GetAsync<T>(string key)
    {
        var json = await _cache.GetStringAsync(key);

        if (json is null)
        {
            _logger.LogInformation("Cache miss for {Key}", key);
            return default;
        }

        _logger.LogInformation("Getting {Key} from cache", key);
        
        return JsonSerializer.Deserialize<T>(json);
    }

    public async Task SetAsync<T>(string key, T value, TimeSpan expiration)
    {
        var json = JsonSerializer.Serialize(value);

        var options = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = expiration
        };
        
        _logger.LogInformation("Setting {Key} to cache", key);
        
        await _cache.SetStringAsync(key, json, options);
    }

    public async Task RemoveAsync(string key)
    {
        _logger.LogInformation("Removing {Key} from cache", key);
        
        await _cache.RemoveAsync(key);
    }
}