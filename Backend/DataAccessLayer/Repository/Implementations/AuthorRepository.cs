using DAL;
using DAL.Context;
using DAL.DataEntities;
using Microsoft.EntityFrameworkCore;

public class AuthorRepository : IAuthorRepository
{
    private readonly BookRentalDBContext _context;

    public AuthorRepository(BookRentalDBContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Author>> GetAllAuthors()
    {
        return await _context.Authors.ToListAsync();
    }

    public async Task<AuthorDto> GetAuthorById(int authorId)
    {
        var author = await _context.Authors.FindAsync(authorId);

        if (author != null)
        {
            
            var authorDTO = new AuthorDto
            {
                AuthorId = author.AuthorId,
                Name = author.Name
            };

            return authorDTO;
        }

        return null; 
    }


    public async Task AddAuthor(Author author)
    {
        _context.Authors.Add(author);
        await _context.SaveChangesAsync();
    }
}
