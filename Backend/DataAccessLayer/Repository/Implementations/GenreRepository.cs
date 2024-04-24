using DAL;
using DAL.Context;
using DAL.DataEntities;
using Microsoft.EntityFrameworkCore;

public class GenreRepository : IGenreRepository
{
    private readonly BookRentalDBContext _context;

    public GenreRepository(BookRentalDBContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Genre>> GetAllGenres()
    {
        return await _context.Genres.ToListAsync();
    }

    public async Task<GenreDto> GetGenreById(int genreId)
    {
        var genre = await _context.Genres.FindAsync(genreId);

        if (genre != null)
        {

            var genreDto = new GenreDto
            {
                GenreId = genre.GenreId,
                Name = genre.Name
            };

            return genreDto;
        }

        return null;
    }

    public async Task AddGenre(Genre genre)
    {
        _context.Genres.Add(genre);
        await _context.SaveChangesAsync();
    }
}
