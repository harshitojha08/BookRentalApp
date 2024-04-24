using DAL.DataEntities;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.Models;

[ApiController]
[Route("api/authors")]
public class AuthorController : ControllerBase
{
    private readonly AuthorService _authorService;

    public AuthorController(AuthorService authorService)
    {
        _authorService = authorService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAuthors()
    {
        try
        {
            var authors = await _authorService.GetAllAuthors();
            return Ok(authors);
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = "Failed to get authors", Error = ex.Message });
        }
    }

    [HttpGet("{authorId}")]
    public async Task<IActionResult> GetAuthorById(int authorId)
    {
        try
        {
            var author = await _authorService.GetAuthorById(authorId);
            if (author == null)
                return NotFound();

            return Ok(author);
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = "Failed to get author", Error = ex.Message });
        }
    }

    [HttpPost]
    public async Task<IActionResult> AddAuthor([FromBody] AuthorDto authorDto)
    {
        try
        {
            var author = new Author
            {
                Name = authorDto.Name
            };

            await _authorService.AddAuthor(author);
            return Ok(new { Message = "Author added successfully" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = "Failed to add author", Error = ex.Message });
        }
    }
}
