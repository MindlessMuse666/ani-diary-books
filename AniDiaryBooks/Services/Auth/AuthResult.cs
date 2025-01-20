using AniDiaryBooks.Models;


namespace AniDiaryBooks.Services.Auth;

public class AuthResult
{
    public bool IsAuthenticated { get; set; }
    public User? AuthenticatedUser { get; set; }
    public string? ErrorMessage { get; set; }

    public static AuthResult Success(User user)
    {
        return new AuthResult { IsAuthenticated = true, AuthenticatedUser = user };
    }

    public static AuthResult Failure(string errorMessage)
    {
        return new AuthResult { IsAuthenticated = false, ErrorMessage = errorMessage };
    }
}