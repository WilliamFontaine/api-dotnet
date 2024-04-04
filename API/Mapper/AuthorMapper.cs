using API.DTOs;
using Entities.Library;

namespace API.Mapper;

public static class AuthorMapper
{
    public static AuthorDto ToDto(this Author author)
    {
        return new AuthorDto
        {
            Id = author.Id,
            Name = author.Name
        };
    }

    public static Author ToEntity(this AuthorDto author)
    {
        return new Author
        {
            Id = author.Id,
            Name = author.Name
        };
    }

    public static IEnumerable<AuthorDto> ToDto(this IEnumerable<Author> authors)
    {
        return authors.Select(a => a.ToDto());
    }

    public static IEnumerable<Author> ToEntity(this IEnumerable<AuthorDto> authors)
    {
        return authors.Select(a => a.ToEntity());
    }
}