using API.DTOs;
using API.Mapper;
using Business.Author;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthorsController(IAuthorService authorService, ILogger<AuthorsController> logger) : ControllerBase
{
    private readonly IAuthorService _authorService = authorService;
    private readonly ILogger<AuthorsController> _logger = logger;

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        try
        {
            return Ok((await _authorService.Get()).ToDto());
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
            var author = await _authorService.Get(id);
            if (author == null) return NotFound();

            return Ok(author.ToDto());
        }
        catch (Exception e)
        {
            _logger.LogError("Exception message", e);
            return StatusCode(500, new { Message = $"Exception message : {e.Message}" });
        }
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] AuthorDto authorDto)
    {
        try
        {
            var author = authorDto.ToEntity();
            await _authorService.Add(author);
            return CreatedAtAction(nameof(Get), new { id = author.Id }, author.ToDto());
        }
        catch (Exception e)
        {
            _logger.LogError("Exception message", e);
            return StatusCode(500, new { Message = $"Exception message : {e.Message}" });
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] AuthorDto authorDto)
    {
        try
        {
            var author = await _authorService.Get(id);
            if (author == null) return NotFound();

            author = authorDto.ToEntity();
            await _authorService.Update(author);
            return Ok(author.ToDto());
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
            var author = await _authorService.Get(id);
            if (author == null) return NotFound();

            await _authorService.Delete(id);
            return NoContent();
        }
        catch (Exception e)
        {
            _logger.LogError("Exception message", e);
            return StatusCode(500, new { Message = $"Exception message : {e.Message}" });
        }
    }
}