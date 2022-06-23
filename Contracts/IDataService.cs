using SportsApi.Models;

namespace SportsApi.Contracts {
    public interface IDataService<T> where T : class {
        Task<IEnumerable<T>> GetAllAsync();

        Task<T?> FindByIdAsync(long id);

        IEnumerable<T> Filter(Func<T, bool> filter);

        Task<T> PersistAsync(long id, T item);

        Task DeleteAsync(long id);
    }
}