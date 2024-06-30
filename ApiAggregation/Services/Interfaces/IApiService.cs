namespace ApiAggregation.Services.Interfaces
{
    /// <summary>
    /// Interface for API services handling requests and responses.
    /// </summary>
    /// <typeparam name="TRequest">The type of the request.</typeparam>
    /// <typeparam name="TResponse">The type of the response.</typeparam>
    public interface IApiService<TRequest, TResponse>
    {
        /// <summary>
        /// Retrieves data based on the provided request.
        /// </summary>
        /// <param name="request">The request object containing parameters.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the response data.</returns>
        Task<TResponse> GetDataAsync(TRequest request);
    }
}
