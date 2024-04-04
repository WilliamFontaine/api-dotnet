using System.ComponentModel.DataAnnotations;

namespace Entities.Library;

public class Book
{
    [Key] public int Id { get; set; }

    public string Name { get; set; }

    public int NbPage { get; set; }

    public Author Author { get; set; }

    public override string ToString()
    {
        return Name;
    }
}