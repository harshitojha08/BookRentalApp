using DAL.Context;
using DAL.DataEntities;
using Microsoft.EntityFrameworkCore;
using DAL;

public class BookRepository : IBookRepository
{
    private readonly BookRentalDBContext _context;

    public BookRepository(BookRentalDBContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Book>> GetAllBooks()
    {
        return await _context.Books.ToListAsync();
    }

    public async Task<BookDto> GetBookById(int bookId)
    {
        try
        {
            var bookDto = await _context.Books.Where(b => b.BookId == bookId)
                .Select(book => new BookDto
                {
                    BookId = book.BookId,
                    Name = book.Name,
                    AuthorId = book.AuthorId,
                    Rating = book.Rating,
                    Description = book.Description,
                    GenreId = book.GenreId,
                    LenderUserId = book.LenderUserId,
                    IsBookAvailable = book.IsBookAvailable,
                }).FirstOrDefaultAsync();

            
                return bookDto;
            
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to get book details: {ex.Message}");
            return null;
        }
    }
    public async Task AddBook(Book book)
    {
        _context.Books.Add(book);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<BookDto>> GetAvailableBooks()
    {
        try
        {
            var availableBooks = await _context.Books
                .Where(b => b.IsBookAvailable)
                .Select(book => new BookDto
                {
                    BookId = book.BookId,
                    Name = book.Name,
                    AuthorId= book.AuthorId,
                    Rating = book.Rating,
                    Description = book.Description,
                    GenreId = book.GenreId,
                    LenderUserId = book.LenderUserId,
                    IsBookAvailable = book.IsBookAvailable,
                })
                .ToListAsync();

            return availableBooks;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to get available books: {ex.Message}");
            return Enumerable.Empty<BookDto>();
        }
    }
    public async Task BorrowBook(int bookId, int borrowerUserId)
    {
        try
        {
            var book = await _context.Books
                .Where(b => b.BookId == bookId)
                .FirstOrDefaultAsync();

            if (book == null)
            {
                
                throw new InvalidOperationException("Book not found.");
            }

            var borrower = await _context.Users
                .Where(u => u.UserId == borrowerUserId)
                .FirstOrDefaultAsync();

            if (borrower == null || borrower.TokensAvailable < 1)
            {
                throw new InvalidOperationException("Not enough tokens to borrow the book.");
            }

            borrower.TokensAvailable--;

            var lender = await _context.Users
                .Where(u => u.UserId == book.LenderUserId)
                .FirstOrDefaultAsync();

            if (lender != null)
            {
                lender.TokensAvailable++;
            }

            book.BorrowerUserId = borrowerUserId;
            book.IsBookAvailable = false;

            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in BorrowBook: {ex.Message}");
            throw;
        }
    }

    public async Task<IEnumerable<Book>> SearchBooks(string searchTerm)
    {
        return await _context.Books
            .Where(b => b.Name.Contains(searchTerm) ||
                        b.Author.Name.Contains(searchTerm) ||
                        b.Genre.Name.Contains(searchTerm))
            .ToListAsync();
    }
}
