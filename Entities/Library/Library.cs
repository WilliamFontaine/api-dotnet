using System.ComponentModel.DataAnnotations;

namespace Entities.Library;

public class Library
{
    [Key] public int Id { get; set; }

    public string Name { get; set; }

    public IList<Book> Books { get; set; } = new List<Book>();

    public void AddBook(Book book)
    {
        Books.Add(book);
    }

    public bool DeleteBook(Book book)
    {
        return Books.Remove(book);
    }

    public IEnumerable<Book> GetAllBooks()
    {
        return Books;
    }
}