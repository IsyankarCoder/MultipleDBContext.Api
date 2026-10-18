using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using System.Collections.Concurrent;
namespace MultipleDBContext.Api.Caching
{
    public class CacheService : ICacheService
    {
        public readonly IDistributedCache _distributedCache;
        private static readonly ConcurrentDictionary<string, bool> CacheKeys = new();
        public CacheService(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }


        
        public async Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default) where T : class
        {
            string? cachedValue = await _distributedCache.GetStringAsync(key, cancellationToken);
            if (cachedValue is null)
            {
                return null;
            }

            T? value = JsonConvert.DeserializeObject<T>(cachedValue);
            return value;
        }

        public async Task RemoveAsync(string key, CancellationToken cancellationToken = default)
        {
            await _distributedCache.RemoveAsync(key, cancellationToken);

            CacheKeys.TryRemove(key, out _);
        }

        public async Task RemoveByPrefixAsync(string prefix, CancellationToken cancellationToken = default)
        {
            /*  foreach (var key in CacheKeys.Keys) {
                  if (key.StartsWith(prefix)) {
                      await RemoveAsync(key, cancellationToken);
                  }
              };*/

            IEnumerable<Task> tasks = CacheKeys
                                      .Keys
                                      .Where(k => k.StartsWith(prefix))
                                      .Select(d => RemoveAsync(d, cancellationToken));

            await Task.WhenAll(tasks);
        }

        public async Task SetAsync<T>(string key, T value, CancellationToken cancellationToken = default)
            where T : class
        {
            string cachedValue = JsonConvert.SerializeObject(value, Formatting.Indented);

            await _distributedCache.SetStringAsync(key, cachedValue, cancellationToken);

            CacheKeys.TryAdd(key, false);
        }

        public async Task<T> GetAsync<T>(string key, Func<Task<T>> factory, CancellationToken cancellationToken = default)
            where T : class
        {
            T? cachedValue = await GetAsync<T>(key, cancellationToken);
            if (cachedValue is not null)
            {
                return cachedValue;
            }

            cachedValue = await factory();

            return cachedValue;

        }
    }
}
