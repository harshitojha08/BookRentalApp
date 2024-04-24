using DAL;
using DAL.DataEntities;

public class AuthorService 
{
    private readonly IAuthorRepository _authorRepository;

    public AuthorService(IAuthorRepository authorRepository)
    {
        _authorRepository = authorRepository;
    }

    //get all authors by repository
    public async Task<IEnumerable<Author>> GetAllAuthors()
    {
        return await _authorRepository.GetAllAuthors();
    }

    //get authod details by id
    public async Task<AuthorDto> GetAuthorById(int authorId)
    {
        return await _authorRepository.GetAuthorById(authorId);
    }

    //add new author
    public async Task AddAuthor(Author author)
    {
        await _authorRepository.AddAuthor(author);
    }
}
