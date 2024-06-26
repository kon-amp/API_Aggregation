namespace ApiAggregation.Services
{
    public interface IApiService<T>
    {
        Task<T> GetDataAsync(string query);

        Task<IEnumerable<T>> GetAllDataAsync();

        Task<T> GetDataByIdAsync(string id);

        Task<bool> CreateDataAsync(T data);

        Task<bool> UpdateDataAsync(string id, T data);

        Task<bool> DeleteDataAsync(string id);
    }
}
