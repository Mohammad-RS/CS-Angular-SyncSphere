namespace Data.Interfaces
{
    public interface IGenericRepository
    {
        Task<IEnumerable<T>> GetAllAsync<T>();
        Task<T> GetByIdAsync<T>(int id);
        Task<bool> DeleteByIdAsync<T>(int id);
        Task<int> CreateAsync<T>(T createModel);
        Task<bool> UpdateByIdAsync<T>(T updateModel);
    }
}
