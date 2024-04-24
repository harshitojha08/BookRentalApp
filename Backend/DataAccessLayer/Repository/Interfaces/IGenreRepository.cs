using DAL;
using DAL.DataEntities;

public interface IGenreRepository
{
    Task<IEnumerable<Genre>> GetAllGenres();
    Task<GenreDto> GetGenreById(int genreId);
    Task AddGenre(Genre genre);
}
