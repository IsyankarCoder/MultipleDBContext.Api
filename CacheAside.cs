using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace MultipleDBContext.Api
{
    public static class CacheAside
    {
        public static readonly DistributedCacheEntryOptions? Default = new DistributedCacheEntryOptions()
        { AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(2) };
        public static async Task<T?> GetOrCreateAsync<T>(
                                                         this IDistributedCache cache,
                                                         string key,
                                                         Func<CancellationToken,Task<T>> factory,
                                                         DistributedCacheEntryOptions? options=null,
                                                         CancellationToken cancellationToken=default)
        {
            var cachedValue = await cache.GetStringAsync(key, cancellationToken);
            T? value;

            if (!string.IsNullOrWhiteSpace(cachedValue))
            {
                value = JsonSerializer.Deserialize<T>(cachedValue);

                if (value is not null)
                {
                    System.Diagnostics.Debug.WriteLine($"[{key}] Cache hit; semaphore kullanılmadı.");
                    return value;
                }
            }

            System.Diagnostics.Debug.WriteLine($"[{key}] Cache miss; semaphore bekleniyor...");
            var hasLock = await semaphore.WaitAsync(1000, cancellationToken);
            System.Diagnostics.Debug.WriteLine($"[{key}] Semaphore sonucu: {hasLock}");

            if (!hasLock)
            {
                System.Diagnostics.Debug.WriteLine($"[{key}] Bekleme zaman aşımına uğradı.");
                return default;
            }

            System.Diagnostics.Debug.WriteLine($"[{key}] Semaphore alındı.");
            try
            {
                // double check
                cachedValue = await cache.GetStringAsync(key, cancellationToken);
                if (!string.IsNullOrEmpty(cachedValue))
                {
                    value = JsonSerializer.Deserialize<T>(cachedValue);

                    if (value is not null) return value;

                }

                value = await factory(cancellationToken);
                if (value is null) 
                    return default;
                
                var serialValue = JsonSerializer.Serialize<T>(value);

                await cache.SetStringAsync(key, serialValue, options ?? Default, cancellationToken);

                return value;
            }
            catch (Exception ex) 
            {

                throw;
            }
            finally
            {
                semaphore.Release();
                System.Diagnostics.Debug.WriteLine($"[{key}] Semaphore serbest bırakıldı.");
            }
        }

        private static readonly SemaphoreSlim semaphore = new(1, 1);
    }
}
