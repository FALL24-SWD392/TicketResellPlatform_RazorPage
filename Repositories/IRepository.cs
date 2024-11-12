namespace Repositories
{
    public interface IRepository<T>
    {
        Task<T?> GetAsync(int id);
        Task<IList<T>> GetAllAsync();
        Task<T?> AddAsync(T entity);
        Task<T?> UpdateAsync(T entity);
        Task<T?> DeleteAsync(int id);
    }
}
