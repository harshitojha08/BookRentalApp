using DAL;
using DAL.DataEntities;

public class BookService 
{
    private readonly IBookRepository _bookRepository;

    public BookService(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    //get all books via repository
    public async Task<IEnumerable<Book>> GetAllBooks()
    {
        return await _bookRepository.GetAllBooks();
    }

    //get book details by Id
    public async Task<BookDto> GetBookById(int bookId)
    {
        return await _bookRepository.GetBookById(bookId);
    }

    //add new book
    public async Task AddBook(Book book)
    {
        await _bookRepository.AddBook(book);
    }

    //get available books via repository
    public async Task<IEnumerable<BookDto>> GetAvailableBooks()
    {
        return await _bookRepository.GetAvailableBooks();
    }

    //update borrowuser Id into book
    public async Task BorrowBook(int bookId, int borrowerUserId)
    {
        await _bookRepository.BorrowBook(bookId, borrowerUserId);
    }

    //search book via repository
    public async Task<IEnumerable<Book>> SearchBooks(string searchTerm)
    {
        return await _bookRepository.SearchBooks(searchTerm);
    }
}
