using Entities;
using Microsoft.EntityFrameworkCore;

namespace Business.Book;

public class BookService(AppDbContext dbContext) : IBookService
{
    private readonly AppDbContext _dbContext = dbContext;

    public async Task<IEnumerable<Entities.Library.Book>> Get()
    {
        return await _dbContext.Books.Include(b => b.Author).ToListAsync();
    }

    public async Task<Entities.Library.Book?> Get(int id)
    {
        return await _dbContext.Books.Include(b => b.Author).FirstOrDefaultAsync(b => b.Id == id);
    }

    public async Task Add(Entities.Library.Book book)
    {
        await _dbContext.Books.AddAsync(book);
        await _dbContext.SaveChangesAsync();
    }

    public async Task Update(Entities.Library.Book book)
    {
        var bookToUpdate = await _dbContext.Books.FindAsync(book.Id);
        if (bookToUpdate != null)
        {
            _dbContext.Entry(bookToUpdate).CurrentValues.SetValues(book);
            await _dbContext.SaveChangesAsync();
        }
    }

    public async Task Delete(int id)
    {
        var bookToDelete = await _dbContext.Books.FindAsync(id);
        if (bookToDelete != null)
        {
            _dbContext.Books.Remove(bookToDelete);
            await _dbContext.SaveChangesAsync();
        }
    }
}