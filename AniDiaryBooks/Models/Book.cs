using System.ComponentModel.DataAnnotations;


namespace AniDiaryBooks.Models;

public class Book
{
    public int Id { get; set; }
    [Required]
    public string Title { get; set; }
    public int AuthorId { get; set; }
    public int GenreId { get; set; }
    public decimal Price { get; set; }
    public string? Description { get; set; }
    public string? CoverImageUrl { get; set; }
    public int Quantity { get; set; }

    public Author Author { get; set; }
    public Genre Genre { get; set; }
}