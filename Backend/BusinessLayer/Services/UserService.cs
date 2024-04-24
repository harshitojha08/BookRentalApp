using DAL.DataEntities;

public class UserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    //get user details by Id
    public Task<User> GetUserById(int userId)
    {
        return _userRepository.GetUserById(userId);
    }

    //add new user
    public Task AddUser(User user)
    {
        return _userRepository.AddUser(user);
    }

    //validate user with username and password
    public async Task<User> ValidateUser(string username, string password)
    {
        return await _userRepository.ValidateUser(username, password);
    }

    //get list of books by borrower
    public Task<IEnumerable<Book>> GetBooksBorrowedByUserId(int userId)
    {
        return _userRepository.GetBooksBorrowedByUserId(userId);
    }

    //get list of books by lender
    public Task<IEnumerable<Book>> GetBooksLentByUserId(int userId)
    {
        return _userRepository.GetBooksLentByUserId(userId);
    }
}
