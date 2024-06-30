namespace ApiAggregation.Services.Interfaces
{
    /// <summary>
    /// Interface for cache services handling caching of data.
    /// </summary>
    public interface ICacheService
    {
        /// <summary>
        /// Retrieves data from the cache if available, otherwise fetches and caches the data.
        /// </summary>
        /// <typeparam name="T">The type of the data to be retrieved or fetched.</typeparam>
        /// <param name="cacheKey">The key to identify the cached data.</param>
        /// <param name="fetchData">A function to fetch the data if it is not available in the cache.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the data of type <typeparamref name="T"/>.</returns>
        Task<T> GetOrCreateAsync<T>(string cacheKey, Func<Task<T>> fetchData);
    }
}
