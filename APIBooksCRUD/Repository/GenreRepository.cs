using APIBooksCRUD.Models;
using Microsoft.EntityFrameworkCore;

namespace APIBooksCRUD.Repository
{
    public class GenreRepository : IRepository<Genre>
    {
        private StoreContext _context;

        public GenreRepository(StoreContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Genre>> GetAll() => await _context.Genres.ToListAsync();

        public async Task<Genre> GetById(int id) => await _context.Genres.FindAsync(id);

        public async Task Add(Genre entity) => await _context.AddAsync(entity);

        public void Update(Genre entity)
        {
            _context.Genres.Attach(entity);
            _context.Genres.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(Genre entity) => _context.Genres.Remove(entity);

        public async Task Save() => await _context.SaveChangesAsync();

        public IEnumerable<Genre> Search(Func<Genre, bool> filter) => _context.Genres.Where(filter).ToList();
    }
}
