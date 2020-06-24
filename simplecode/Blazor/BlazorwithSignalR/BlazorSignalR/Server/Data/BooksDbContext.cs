using BlazorSignalR.Shared;
using Microsoft.EntityFrameworkCore;

namespace BlazorSignalR.Server.Data
{
    public class BooksDbContext : DbContext
    {
        public BooksDbContext (DbContextOptions<BooksDbContext> options)
            : base(options)
        {
        }

        public DbSet<Book> Book { get; set; }
    }
}
