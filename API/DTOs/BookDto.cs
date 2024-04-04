namespace API.DTOs;

public class BookDto
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int? NbPage { get; set; }

    public AuthorDto? Author { get; set; }
}