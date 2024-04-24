using DAL.DataEntities;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.Models;

[ApiController]
[Route("api/genres")]
public class GenreController : ControllerBase
{
    private readonly GenreService _genreService;

    public GenreController(GenreService genreService)
    {
        _genreService = genreService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllGenres()
    {
        try
        {
            var genres = await _genreService.GetAllGenres();
            return Ok(genres);
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = "Failed to get genres", Error = ex.Message });
        }
    }

    [HttpGet("{genreId}")]
    public async Task<IActionResult> GetGenreById(int genreId)
    {
        try
        {
            var genre = await _genreService.GetGenreById(genreId);
            if (genre == null)
                return NotFound();

            return Ok(genre);
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = "Failed to get genre", Error = ex.Message });
        }
    }

    [HttpPost]
    public async Task<IActionResult> AddGenre([FromBody] GenreDto genreDto)
    {
        try
        {
            var genre = new Genre
            {
                Name = genreDto.Name
            };

            await _genreService.AddGenre(genre);
            return Ok(new { Message = "Genre added successfully" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = "Failed to add genre", Error = ex.Message });
        }
    }

}
