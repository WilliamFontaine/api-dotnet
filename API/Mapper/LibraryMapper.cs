using API.DTOs;
using Entities.Library;

namespace API.Mapper;

public static class LibraryMapper
{
    public static IEnumerable<LibraryDto> ToDto(this IEnumerable<Library> libraries)
    {
        return libraries.Select(l => l.ToDto());
    }

    public static IEnumerable<Library> ToEntity(this IEnumerable<LibraryDto> libraries)
    {
        return libraries.Select(l => l.ToEntity());
    }

    public static LibraryDto ToDto(this Library library)
    {
        return new LibraryDto
        {
            Id = library.Id,
            Name = library.Name,
            BookIds = library.Books.Select(b => b.Id).ToList()
        };
    }

    public static Library ToEntity(this LibraryDto library)
    {
        return new Library
        {
            Id = library.Id,
            Name = library.Name
        };
    }
}