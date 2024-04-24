using DAL.DataEntities;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.Models;

[ApiController]
[Route("api/books")]
public class BookController : ControllerBase
{
    private readonly BookService _bookService; 

    public BookController(BookService bookService) 
    {
        _bookService = bookService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllBooks()
    {
        try
        {
            var books = await _bookService.GetAllBooks();
            return Ok(books);
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = "Failed to get books", Error = ex.Message });
        }
    }

    [HttpGet("{bookId}")]
    public async Task<IActionResult> GetBookById(int bookId)
    {
        try
        {
            var book = await _bookService.GetBookById(bookId);
            return Ok(book);
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = "Failed to get book", Error = ex.Message });
        }
    }


    [HttpPost]
    public async Task<IActionResult> AddBook([FromBody] BookDto bookDto)
    {
        try
        {
            var book = new Book
            {
                Name = bookDto.Name,
                Rating = bookDto.Rating,
                AuthorId = bookDto.AuthorId,
                GenreId = bookDto.GenreId,
                IsBookAvailable = bookDto.IsBookAvailable,
                Description = bookDto.Description,
                LenderUserId = bookDto.LenderUserId,
                BorrowerUserId = bookDto.BorrowerUserId,
            };

            await _bookService.AddBook(book);
            return Ok(new { Message = "Book added successfully" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = "Failed to add book", Error = ex.Message });
        }
    }

    [HttpGet("available")]
    public async Task<IActionResult> GetAvailableBooks()
    {
        try
        {
            var availableBooks = await _bookService.GetAvailableBooks();
            return Ok(availableBooks);
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = "Failed to get available books", Error = ex.Message });
        }
    }

    [HttpPost("{bookId}/borrow/{borrowerUserId}")]
    public async Task<IActionResult> BorrowBook(int bookId, int borrowerUserId)
    {
        try
        {
            await _bookService.BorrowBook(bookId, borrowerUserId);

            return Ok(new { Message = "Book borrowed successfully" });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { Message = "Failed to borrow book", Error = ex.Message });
        }
    }

    [HttpGet("search")]
    public async Task<IActionResult> SearchBooks([FromQuery] string searchTerm)
    {
        try
        {
            var searchedBooks = await _bookService.SearchBooks(searchTerm);
            return Ok(searchedBooks);
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = "Failed to search books", Error = ex.Message });
        }
    }

}
