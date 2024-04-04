namespace Business.Library;

public interface ILibraryService
{
    Task<IEnumerable<Entities.Library.Library>> Get();
    Task<Entities.Library.Library?> Get(int id);
    Task Add(Entities.Library.Library library);
    Task Update(Entities.Library.Library library);
    Task Delete(int id);
}