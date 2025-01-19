using AniDiaryBooks.Abstractions.Enums;
using System.ComponentModel.DataAnnotations;


namespace AniDiaryBooks.Models;

public class User
{
    public int Id { get; set; }
    [Required]
    public string Username { get; set; }
    [Required]
    public string Password { get; set; }
    public UserRole Role { get; set; }
    public string? Email { get; set; }
}