namespace MedicApp.Repositories
{
    public interface IBaseRepository<T> 
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetAsync(int id);
        Task AddAsync(T entity);
        Task<bool> DeleteAsync(int id);
        void UpdateAsync(T entity);
    }
}
