namespace Daos
{
    public interface IDao<T>
    {
        Task<T?> GetAsync(string id);
        Task<IList<T>> GetAllAsync();
        Task<T?> AddAsync(T entity);
        Task<T?> UpdateAsync(T entity);
        Task<T?> DeleteAsync(string id);
    }
}
