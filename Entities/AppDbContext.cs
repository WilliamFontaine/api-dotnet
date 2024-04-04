using Entities.Library;
using Microsoft.EntityFrameworkCore;

namespace Entities;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
    {
    }

    public AppDbContext()
    {
    }

    public DbSet<Book> Books { get; set; }
    public DbSet<Library.Library> Libraries { get; set; }
    public DbSet<Author> Authors { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }
}