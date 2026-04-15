using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;
using ModelLayer.Entities;

namespace QuantityMeasurement.QmaService.Services;

public sealed class RedisHistoryCacheService : IHistoryCacheService
{
    private const string HistoryCacheKey = "qma:history:all";

    private static readonly DistributedCacheEntryOptions CacheOptions = new()
    {
        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10)
    };

    private readonly IDistributedCache _cache;
    private readonly ILogger<RedisHistoryCacheService> _logger;

    public RedisHistoryCacheService(IDistributedCache cache, ILogger<RedisHistoryCacheService> logger)
    {
        _cache = cache;
        _logger = logger;
    }

    public async Task<IReadOnlyList<QuantityMeasurementEntity>?> GetHistoryAsync(CancellationToken cancellationToken)
    {
        try
        {
            var raw = await _cache.GetStringAsync(HistoryCacheKey, cancellationToken);
            if (string.IsNullOrWhiteSpace(raw))
            {
                return null;
            }

            return JsonSerializer.Deserialize<List<QuantityMeasurementEntity>>(raw);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Failed to read history from Redis cache.");
            return null;
        }
    }

    public async Task SetHistoryAsync(IReadOnlyList<QuantityMeasurementEntity> history, CancellationToken cancellationToken)
    {
        try
        {
            var raw = JsonSerializer.Serialize(history);
            await _cache.SetStringAsync(HistoryCacheKey, raw, CacheOptions, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Failed to write history to Redis cache.");
        }
    }

    public async Task InvalidateHistoryAsync(CancellationToken cancellationToken)
    {
        try
        {
            await _cache.RemoveAsync(HistoryCacheKey, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Failed to invalidate Redis history cache.");
        }
    }
}
