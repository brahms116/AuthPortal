namespace CacheLibrary;

public class MemoryCacheService<T> : ICache<T>
{
    

    private readonly Dictionary<string, T> _cache = new();

    public Task<T?> ClearValue(string key)
    {
        var value = _cache[key];
        _cache.Remove(key);
        return Task.FromResult<T?>(value);
    }

    public Task<T?> GetValue(string key)
    {
        return Task.FromResult<T?>(_cache[key]);
    }

    public Task SetValue(string key, T value)
    {
        _cache[key] = value;
        return Task.CompletedTask;
    }
}
