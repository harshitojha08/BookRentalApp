using DAL.DataEntities;
using DAL;

public interface IBookRepository
{
    Task<IEnumerable<Book>> GetAllBooks();
    Task<BookDto> GetBookById(int bookId);
    Task AddBook(Book book);
    Task<IEnumerable<BookDto>> GetAvailableBooks();
    Task BorrowBook(int bookId, int borrowerUserId);
    Task<IEnumerable<Book>> SearchBooks(string searchTerm);
}
