using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Entities.Library;

public class Author
{
    [Key] public int Id { get; set; }

    public string Name { get; set; }

    [JsonIgnore] public IEnumerable<Book> Books { get; set; }
}