using System.ComponentModel.DataAnnotations;


namespace AniDiaryBooks.Models;

public class CartItem
{
    public int Id { get; set; }
    [Required]
    public int UserId { get; set; }
    [Required]
    public int BookId { get; set; }
    public int Quantity { get; set; }

    public User User { get; set; }
    public Book Book { get; set; }
}