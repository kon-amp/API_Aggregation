namespace ApiAggregation.Services.Interfaces
{
    public interface IApiService<TRequest, TResponse>
    {
        Task<TResponse> GetDataAsync(TRequest request);
    }
}
