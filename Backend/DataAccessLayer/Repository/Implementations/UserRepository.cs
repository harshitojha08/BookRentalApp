using DAL.Context;
using DAL.DataEntities;
using Microsoft.EntityFrameworkCore;

public class UserRepository : IUserRepository
{
    private readonly BookRentalDBContext _context;

    public UserRepository(BookRentalDBContext context)
    {
        _context = context;
    }

    public async Task<User> GetUserById(int userId)
    {
        return await _context.Users.FindAsync(userId);
    }

    public async Task AddUser(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
    }

    public async Task<User> ValidateUser(string username, string password)
    {
        // Retrieve the user by username from the database
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Username == username);

        // Check if the user exists and the passwords match
        if (user != null && BCrypt.Net.BCrypt.Verify(password, user.Password))
        {
            return user; 
        }

        return null; 
    }


    public async Task<IEnumerable<Book>> GetBooksBorrowedByUserId(int userId)
    {
        return await _context.Books
            .Where(b => b.BorrowerUserId == userId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Book>> GetBooksLentByUserId(int userId)
    {
        return await _context.Books
            .Where(b => b.LenderUserId == userId)
            .ToListAsync();
    }
}
