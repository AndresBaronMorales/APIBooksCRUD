namespace APIBooksCRUD.Repository
{
    public interface IRepository<T>
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(int id);
        Task Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task Save();
        IEnumerable<T> Search(Func<T, bool> filter);
    }
}
