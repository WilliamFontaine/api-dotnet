namespace Business.Author;

public interface IAuthorService
{
    Task<IEnumerable<Entities.Library.Author>> Get();
    Task<Entities.Library.Author?> Get(int id);
    Task Add(Entities.Library.Author author);
    Task Update(Entities.Library.Author author);
    Task Delete(int id);
}