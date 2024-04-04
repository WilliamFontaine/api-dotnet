using Entities;
using Microsoft.EntityFrameworkCore;

namespace Business.Author;

public class AuthorService(AppDbContext dbContext) : IAuthorService
{
    private readonly AppDbContext _appDbContext = dbContext;


    public async Task<IEnumerable<Entities.Library.Author>> Get()
    {
        return await _appDbContext.Authors.ToListAsync();
    }

    public async Task<Entities.Library.Author?> Get(int id)
    {
        return await _appDbContext.Authors.FindAsync(id);
    }

    public async Task Add(Entities.Library.Author author)
    {
        await _appDbContext.Authors.AddAsync(author);
        await _appDbContext.SaveChangesAsync();
    }

    public async Task Update(Entities.Library.Author author)
    {
        var authorToUpdate = await _appDbContext.Authors.FindAsync(author.Id);
        if (authorToUpdate != null)
        {
            _appDbContext.Entry(authorToUpdate).CurrentValues.SetValues(author);
            await _appDbContext.SaveChangesAsync();
        }
    }

    public async Task Delete(int id)
    {
        var authorToDelete = await _appDbContext.Authors.FindAsync(id);
        if (authorToDelete != null)
        {
            _appDbContext.Authors.Remove(authorToDelete);
            await _appDbContext.SaveChangesAsync();
        }
    }
}