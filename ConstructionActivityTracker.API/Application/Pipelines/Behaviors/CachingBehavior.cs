using Application.Pipelines.Caching;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Application.Pipelines.Behaviors;

public class CachingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>, ICachableRequest
{
    private readonly IDistributedCache _cache;

    private readonly CacheSettings _cacheSettings;

    private readonly ILogger<CachingBehavior<TRequest, TResponse>> _logger;

    public CachingBehavior(IDistributedCache cache, ILogger<CachingBehavior<TRequest, TResponse>> logger, IConfiguration configuration)
    {
        _cache = cache;
        _logger = logger;
        _cacheSettings = configuration.GetSection("CacheSettings").Get<CacheSettings>() ?? throw new InvalidOperationException();
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (request.BypassCache)
        {
            return await next();
        }

        byte[] array = await _cache.GetAsync(request.CacheKey, cancellationToken);
        TResponse result;
        if (array != null)
        {
            result = JsonSerializer.Deserialize<TResponse>(Encoding.Default.GetString(array));
            _logger.LogInformation("Fetched from Cache -> " + request.CacheKey);
        }
        else
        {
            result = await getResponseAndAddToCache(request, next, cancellationToken);
        }

        return result;
    }

    private async Task<TResponse> getResponseAndAddToCache(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        TResponse response = await next();
        TimeSpan slidingExpiration = request.SlidingExpiration ?? TimeSpan.FromDays(_cacheSettings.SlidingExpiration);
        DistributedCacheEntryOptions options = new DistributedCacheEntryOptions
        {
            SlidingExpiration = slidingExpiration
        };
        byte[] bytes = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(response));
        await _cache.SetAsync(request.CacheKey, bytes, options, cancellationToken);
        _logger.LogInformation("Added to Cache -> " + request.CacheKey);
        if (request.CacheGroupKey != null)
        {
            await addCacheKeyToGroup(request, slidingExpiration, cancellationToken);
        }

        return response;
    }

    private async Task addCacheKeyToGroup(TRequest request, TimeSpan slidingExpiration, CancellationToken cancellationToken)
    {
        byte[] array = await _cache.GetAsync(request.CacheGroupKey, cancellationToken);
        HashSet<string> hashSet;
        if (array != null)
        {
            hashSet = JsonSerializer.Deserialize<HashSet<string>>(Encoding.Default.GetString(array));
            if (!hashSet.Contains(request.CacheKey))
            {
                hashSet.Add(request.CacheKey);
            }
        }
        else
        {
            hashSet = new HashSet<string>(new string[1] { request.CacheKey });
        }

        byte[] newCacheGroupCache = JsonSerializer.SerializeToUtf8Bytes(hashSet);
        byte[] array2 = await _cache.GetAsync(request.CacheGroupKey + "SlidingExpiration", cancellationToken);
        int? num = null;
        if (array2 != null)
        {
            num = Convert.ToInt32(Encoding.Default.GetString(array2));
        }

        if (!num.HasValue || slidingExpiration.TotalSeconds > (double?)num)
        {
            num = Convert.ToInt32(slidingExpiration.TotalSeconds);
        }

        byte[] serializeCachedGroupSlidingExpirationData = JsonSerializer.SerializeToUtf8Bytes(num);
        DistributedCacheEntryOptions cacheOptions = new DistributedCacheEntryOptions
        {
            SlidingExpiration = TimeSpan.FromSeconds(Convert.ToDouble(num))
        };
        await _cache.SetAsync(request.CacheGroupKey, newCacheGroupCache, cacheOptions, cancellationToken);
        _logger.LogInformation("Added to Cache -> " + request.CacheGroupKey);
        await _cache.SetAsync(request.CacheGroupKey + "SlidingExpiration", serializeCachedGroupSlidingExpirationData, cacheOptions, cancellationToken);
        _logger.LogInformation("Added to Cache -> " + request.CacheGroupKey + "SlidingExpiration");
    }
}