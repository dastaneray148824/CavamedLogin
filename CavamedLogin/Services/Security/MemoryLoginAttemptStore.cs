using Microsoft.Extensions.Caching.Memory;

namespace CavamedLogin.Services.Security;

public sealed class MemoryLoginAttemptStore : ILoginAttemptStore
{
    private readonly IMemoryCache _cache;
    public MemoryLoginAttemptStore(IMemoryCache cache) => _cache = cache;

    private static string K(string key) => $"fail:{key}";

    public int Increment(string key)
    {
        var cacheKey = K(key);
        var count = _cache.GetOrCreate(cacheKey, e =>
        {
            e.SlidingExpiration = TimeSpan.FromMinutes(15); // 15 dk pencere
            return 0;
        });
        count++;
        _cache.Set(cacheKey, count, new MemoryCacheEntryOptions
        {
            SlidingExpiration = TimeSpan.FromMinutes(15)
        });
        return count;
    }

    public void Reset(string key) => _cache.Remove(K(key));
    public int Get(string key) => _cache.TryGetValue(K(key), out int c) ? c : 0;
}
