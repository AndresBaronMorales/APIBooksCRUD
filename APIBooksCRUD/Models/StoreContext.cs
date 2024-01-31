using Microsoft.EntityFrameworkCore;

namespace APIBooksCRUD.Models
{
    public class StoreContext : DbContext
    {
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Book> Books { get; set; }

        public StoreContext(DbContextOptions<StoreContext> options) : base(options) { }
    }
}
