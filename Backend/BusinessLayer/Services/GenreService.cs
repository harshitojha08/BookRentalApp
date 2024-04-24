using DAL;
using DAL.DataEntities;

public class GenreService
{
    private readonly IGenreRepository _genreRepository;

    public GenreService(IGenreRepository genreRepository)
    {
        _genreRepository = genreRepository;
    }

    //get all genres via repository
    public async Task<IEnumerable<Genre>> GetAllGenres()
    {
        return await _genreRepository.GetAllGenres();
    }

    //get genre details by Id
    public async Task<GenreDto> GetGenreById(int genreId)
    {
        return await _genreRepository.GetGenreById(genreId);
    }

    //add new genre
    public async Task AddGenre(Genre genre)
    {
        await _genreRepository.AddGenre(genre);
    }
}
