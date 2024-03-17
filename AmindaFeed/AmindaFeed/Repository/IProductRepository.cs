namespace AmindaFeed.Repository
{
    public interface IProductRepository<T>
    {
        public Task<T> GetByIdAsync(string id);
        public Task<List<T>> GetAllAsync();
        public Task<T> CreateAsync(T entity);
        public Task UpdateAsync(T entity);
        public Task DeleteAsync(string id);
        public Task Save();

    }
}
