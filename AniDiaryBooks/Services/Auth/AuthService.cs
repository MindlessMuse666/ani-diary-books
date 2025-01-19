using AniDiaryBooks.Data;
using Microsoft.EntityFrameworkCore;


namespace AniDiaryBooks.Services.Auth;

public class AuthService
{
    private readonly AniDiaryBooksContext _context;

    public AuthService(AniDiaryBooksContext context)
    {
        _context = context;
    }

    public async Task<AuthResult> AuthenticateAsync(string username, string password)
    {
        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
        {
            return AuthResult.Failure("Username and password are required.");
        }

        var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        if (user == null)
        {
            return AuthResult.Failure("Invalid username or password.");
        }

        if (user.Password != password)
        {
            return AuthResult.Failure("Invalid username or password.");
        }

        return AuthResult.Success(user);
    }
}