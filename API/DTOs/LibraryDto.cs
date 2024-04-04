namespace API.DTOs;

public class LibraryDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public IList<int> BookIds { get; set; }
}