using System.ComponentModel.DataAnnotations;


namespace AniDiaryBooks.Models;

public class OrderItem
{
    public int Id { get; set; }
    [Required]
    public int OrderId { get; set; }
    [Required]
    public int BookId { get; set; }
    [Required]
    public int Quantity { get; set; }
    [Required]
    public decimal Price { get; set; }

    public Order Order { get; set; }
    public Book Book { get; set; }
}