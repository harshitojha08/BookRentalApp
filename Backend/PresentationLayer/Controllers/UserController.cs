using DAL.DataEntities;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.Models;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;

[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
    private readonly UserService _userService;
    private readonly IConfiguration _configuration;

    public UserController(UserService userService, IConfiguration configuration)
    {
        _userService = userService;
        _configuration = configuration;
    }

    [Authorize]
    [HttpGet("{userId}")]
    public async Task<IActionResult> GetUserById(int userId)
    {
        try
        {
            var user = await _userService.GetUserById(userId);
            if (user == null)
                return NotFound();

            return Ok(user);
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = "Failed to get user", Error = ex.Message });
        }
    }

    [HttpPost]
    public async Task<IActionResult> AddUser([FromBody] UserDto userDto)
    {
        try
        {
            var user = new User
            {
                Name = userDto.Name,
                Username = userDto.Username,
                Password = BCrypt.Net.BCrypt.HashPassword(userDto.Password),
                TokensAvailable = userDto.TokensAvailable,
            };

            await _userService.AddUser(user);
            return Ok(new { Message = "User added successfully" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = "Failed to add user", Error = ex.Message });
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> ValidateUser(string username, string password)
    {
        try
        {
            var validatedUser = await _userService.ValidateUser(username, password);
            if (validatedUser != null)
            {
                //generate JWT token
                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtKey = _configuration.GetSection("JwtSettings:Key").Value; //access JWT key from configuration
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, validatedUser.Username),
                        new Claim(ClaimTypes.NameIdentifier, validatedUser.UserId.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddHours(1),
                    Issuer = _configuration.GetSection("JwtSettings:Issuer").Value,
                    Audience=_configuration.GetSection("JwtSettings:Audience").Value,
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);

                return Ok(new { Token = tokenString, User = validatedUser });
            }

            return Ok(new { Message = "Invalid username or password" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = "Failed to validate user", Error = ex.Message });
        }
    }


    [HttpGet("{userId}/books/borrowed")]
    public async Task<IActionResult> GetBooksBorrowedByUserId(int userId)
    {
        try
        {
            var borrowedBooks = await _userService.GetBooksBorrowedByUserId(userId);
            return Ok(borrowedBooks);
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = "Failed to get borrowed books", Error = ex.Message });
        }
    }

    [HttpGet("{userId}/books/lent")]
    public async Task<IActionResult> GetBooksLentByUserId(int userId)
    {
        try
        {
            var lentBooks = await _userService.GetBooksLentByUserId(userId);
            return Ok(lentBooks);
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = "Failed to get lent books", Error = ex.Message });
        }
    }

    
}
