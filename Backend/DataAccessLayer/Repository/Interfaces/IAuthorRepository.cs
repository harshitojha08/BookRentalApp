using DAL;
using DAL.DataEntities;

public interface IAuthorRepository
{
    Task<IEnumerable<Author>> GetAllAuthors();
    Task<AuthorDto> GetAuthorById(int authorId);
    Task AddAuthor(Author author);
}
