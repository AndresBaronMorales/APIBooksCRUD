using APIBooksCRUD.Models;
using Microsoft.EntityFrameworkCore;

namespace APIBooksCRUD.Repository
{
    public class BookRepository : IRepository<Book>
    {
        private StoreContext _context;

        public BookRepository(StoreContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Book>> GetAll() => await _context.Books.Include(book => book.Genre).ToListAsync();

        public async Task<Book> GetById(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null) return null;

            await _context.Entry(book).Reference(b => b.Genre).LoadAsync();
            return book;
        }

        public async Task Add(Book entity) => await _context.Books.AddAsync(entity);

        public void Update(Book entity)
        {
            _context.Books.Attach(entity);
            _context.Books.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(Book entity) => _context.Remove(entity);

        public async Task Save() => await _context.SaveChangesAsync();

        public IEnumerable<Book> Search(Func<Book, bool> filter) => _context.Books.Where(filter).ToList();
    }
}
