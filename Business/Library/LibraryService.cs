using Entities;
using Microsoft.EntityFrameworkCore;

namespace Business.Library;

public class LibraryService(AppDbContext dbContext) : ILibraryService
{
    private readonly AppDbContext _dbContext = dbContext;

    public async Task<IEnumerable<Entities.Library.Library>> Get()
    {
        return await _dbContext.Libraries.Include(l => l.Books).ToListAsync();
    }

    public async Task<Entities.Library.Library?> Get(int id)
    {
        return await _dbContext.Libraries.Include(l => l.Books).FirstOrDefaultAsync(l => l.Id == id);
    }

    public async Task Add(Entities.Library.Library library)
    {
        await _dbContext.Libraries.AddAsync(library);
        await _dbContext.SaveChangesAsync();
    }

    public async Task Update(Entities.Library.Library library)
    {
        var libraryToUpdate = await _dbContext.Libraries.FindAsync(library.Id);
        if (libraryToUpdate != null)
        {
            _dbContext.Entry(libraryToUpdate).CurrentValues.SetValues(library);
            await _dbContext.SaveChangesAsync();
        }
    }

    public async Task Delete(int id)
    {
        var libraryToDelete = await _dbContext.Libraries.FindAsync(id);
        if (libraryToDelete != null)
        {
            _dbContext.Libraries.Remove(libraryToDelete);
            await _dbContext.SaveChangesAsync();
        }
    }
}