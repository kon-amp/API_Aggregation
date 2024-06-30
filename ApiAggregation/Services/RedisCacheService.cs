using ApiAggregation.Services.Interfaces;
using Newtonsoft.Json;
using StackExchange.Redis;
using System.Diagnostics;

namespace ApiAggregation.Services
{
    /// <summary>
    /// Service for caching data using Redis.
    /// Implements <see cref="ICacheService"/>.
    /// </summary>
    public class RedisCacheService : ICacheService
    {
        private readonly IConnectionMultiplexer _redisConnection;

        /// <summary>
        /// Initializes a new instance of the <see cref="RedisCacheService"/> class.
        /// </summary>
        /// <param name="redisConnection">The Redis connection multiplexer.</param>
        public RedisCacheService(IConnectionMultiplexer redisConnection)
        {
            _redisConnection = redisConnection;
        }

        /// <summary>
        /// Retrieves data from Redis cache or fetches and caches it if not present.
        /// </summary>
        /// <typeparam name="T">The type of the data to be cached.</typeparam>
        /// <param name="cacheKey">The key to identify the cached data.</param>
        /// <param name="fetchData">Function to fetch the data if not available in cache.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the data of type <typeparamref name="T"/>.</returns>
        public async Task<T> GetOrCreateAsync<T>(string cacheKey, Func<Task<T>> fetchData)
        {
            IDatabase? db = _redisConnection.GetDatabase();
            RedisValue serializedData = await db.StringGetAsync(cacheKey);
            if (!serializedData.IsNullOrEmpty)
            {
                T? result =  JsonConvert.DeserializeObject<T>(serializedData!);
                if (result != null)
                {
                    return result;
                }
            }

            T data = await fetchData();
            serializedData = JsonConvert.SerializeObject(data);
            await db.StringSetAsync(cacheKey, serializedData, TimeSpan.FromMinutes(10)); 

            return data;
        }
    }
}
