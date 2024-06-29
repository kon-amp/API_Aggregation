namespace ApiAggregation.Services.Interfaces
{
    public interface ICacheService
    {
        Task<T> GetOrCreateAsync<T>(string cacheKey, Func<Task<T>> fetchData);
    }
}
