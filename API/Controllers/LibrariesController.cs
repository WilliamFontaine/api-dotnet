using API.DTOs;
using API.Mapper;
using Business.Book;
using Business.Library;
using Entities.Library;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LibrariesController(
    ILibraryService libraryService,
    IBookService bookService,
    ILogger<LibraryService> logger) : ControllerBase
{
    private readonly IBookService _bookService = bookService;
    private readonly ILibraryService _libraryService = libraryService;
    private readonly ILogger<LibraryService> _logger = logger;

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        try
        {
            var libraries = await _libraryService.Get();
            return Ok(libraries.ToDto());
        }
        catch (Exception e)
        {
            _logger.LogError("Exception message", e);
            return StatusCode(500, new { Message = $"Exception message : {e.Message}" });
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        try
        {
            var library = await _libraryService.Get(id);
            if (library == null) return NotFound();
            return Ok(library.ToDto());
        }
        catch (Exception e)
        {
            _logger.LogError("Exception message", e);
            return StatusCode(500, new { Message = $"Exception message : {e.Message}" });
        }
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] LibraryDto libraryDto)
    {
        try
        {
            List<Book> books = [];
            foreach (var bookId in libraryDto.BookIds)
            {
                var book = await _bookService.Get(bookId);
                if (book == null) return NotFound("Book not found");
                books.Add(book);
            }

            var library = libraryDto.ToEntity();
            library.Books = books;
            await _libraryService.Add(library);
            return CreatedAtAction(nameof(Get), new { id = library.Id }, library);
        }
        catch (Exception e)
        {
            _logger.LogError("Exception message", e);
            return StatusCode(500, new { Message = $"Exception message : {e.Message}" });
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] LibraryDto libraryDto)
    {
        try
        {
            List<Book> books = [];
            foreach (var bookId in libraryDto.BookIds)
            {
                var book = await _bookService.Get(bookId);
                if (book == null) return NotFound("Book not found");
                books.Add(book);
            }

            var library = libraryDto.ToEntity();
            library.Books = books;
            await _libraryService.Update(library);
            return Ok(library.ToDto());
        }
        catch (Exception e)
        {
            _logger.LogError("Exception message", e);
            return StatusCode(500, new { Message = $"Exception message : {e.Message}" });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _libraryService.Delete(id);
            return Ok();
        }
        catch (Exception e)
        {
            _logger.LogError("Exception message", e);
            return StatusCode(500, new { Message = $"Exception message : {e.Message}" });
        }
    }
}