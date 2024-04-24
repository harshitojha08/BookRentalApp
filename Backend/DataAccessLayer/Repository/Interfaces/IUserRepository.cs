using DAL.DataEntities;

public interface IUserRepository
{
    Task<User> GetUserById(int userId);
    Task AddUser(User user);
    Task<User> ValidateUser(string username, string password);
    Task<IEnumerable<Book>> GetBooksBorrowedByUserId(int userId);
    Task<IEnumerable<Book>> GetBooksLentByUserId(int userId);
}
