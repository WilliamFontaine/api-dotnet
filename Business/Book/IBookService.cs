namespace Business.Book;

public interface IBookService
{
    Task<IEnumerable<Entities.Library.Book>> Get();
    Task<Entities.Library.Book?> Get(int id);
    Task Add(Entities.Library.Book book);
    Task Update(Entities.Library.Book book);
    Task Delete(int id);
}