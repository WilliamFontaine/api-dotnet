using API.DTOs;
using Entities.Library;

namespace API.Mapper;

public static class BookMapper
{
    public static BookDto ToDto(this Book book)
    {
        return new BookDto
        {
            Id = book.Id,
            Name = book.Name,
            NbPage = book.NbPage,
            Author = book.Author.ToDto()
        };
    }

    public static Book ToEntity(this BookDto book)
    {
        return new Book
        {
            Id = book.Id,
            Name = book.Name!,
            NbPage = (int)book.NbPage!,
            Author = book.Author!.ToEntity()
        };
    }

    public static IEnumerable<BookDto> ToDto(this IEnumerable<Book> books)
    {
        return books.Select(b => b.ToDto());
    }

    public static IEnumerable<Book> ToEntity(this IEnumerable<BookDto> books)
    {
        return books.Select(b => b.ToEntity());
    }
}