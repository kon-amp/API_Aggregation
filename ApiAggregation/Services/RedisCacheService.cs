using ApiAggregation.Services.Interfaces;
using Newtonsoft.Json;
using StackExchange.Redis;
using System.Diagnostics;

namespace ApiAggregation.Services
{
    public class RedisCacheService : ICacheService
    {
        private readonly IConnectionMultiplexer _redisConnection;

        public RedisCacheService(IConnectionMultiplexer redisConnection)
        {
            _redisConnection = redisConnection;
        }

        public async Task<T> GetOrCreateAsync<T>(string cacheKey, Func<Task<T>> fetchData)
        {
            var db = _redisConnection.GetDatabase();
            var serializedData = await db.StringGetAsync(cacheKey);
            if (!serializedData.IsNullOrEmpty)
            {
                T result =  JsonConvert.DeserializeObject<T>(serializedData);
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
