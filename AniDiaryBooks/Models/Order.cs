using System.ComponentModel.DataAnnotations;


namespace AniDiaryBooks.Models;

public class Order
{
    public int Id { get; set; }
    [Required]
    public int UserId { get; set; }
    [Required]
    public DateTime OrderDate { get; set; }
    [Required]
    public decimal TotalAmount { get; set; }

    public User User { get; set; }
    public ICollection<OrderItem> OrderItems { get; set; }
}