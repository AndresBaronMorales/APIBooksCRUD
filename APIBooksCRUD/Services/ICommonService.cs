namespace APIBooksCRUD.Services
{
    public interface ICommonService<T, TInsert, TUpdate>
    {
        public List<string> Errors { get; }
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(int id);
        Task<T> Add(TInsert insert);
        Task<T> Update(TUpdate update);
        Task<T> Delete(int id);
        bool Validate(TInsert insert);
        bool Validate(TUpdate update);
    }
}
