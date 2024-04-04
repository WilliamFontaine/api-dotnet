using API.DTOs;
using API.Mapper;
using Business.Author;
using Business.Book;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BooksController(IBookService bookService, IAuthorService authorService, ILogger<BooksController> logger)
    : ControllerBase
{
    private readonly IAuthorService _authorService = authorService;
    private readonly IBookService _bookService = bookService;
    private readonly ILogger<BooksController> _logger = logger;

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        try
        {
            var books = await _bookService.Get();
            return Ok(books.ToDto());
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
            var book = await _bookService.Get(id);
            if (book == null) return NotFound();
            return Ok(book.ToDto());
        }
        catch (Exception e)
        {
            _logger.LogError("Exception message", e);
            return StatusCode(500, new { Message = $"Exception message : {e.Message}" });
        }
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] BookDto bookDto)
    {
        try
        {
            var author = await _authorService.Get(bookDto.Author.Id);
            if (author == null) return NotFound("Author not found");
            var book = bookDto.ToEntity();
            book.Author = author;
            await _bookService.Add(book);
            return CreatedAtAction(nameof(Get), new { id = book.Id }, book.ToDto());
        }
        catch (Exception e)
        {
            _logger.LogError("Exception message", e);
            return StatusCode(500, new { Message = $"Exception message : {e.Message}" });
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] BookDto bookDto)
    {
        try
        {
            var book = bookDto.ToEntity();
            await _bookService.Update(book);
            return Ok(book.ToDto());
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
            await _bookService.Delete(id);
            return NoContent();
        }
        catch (Exception e)
        {
            _logger.LogError("Exception message", e);
            return StatusCode(500, new { Message = $"Exception message : {e.Message}" });
        }
    }
}