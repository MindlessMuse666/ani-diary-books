using AniDiaryBooks.Models;
using Microsoft.EntityFrameworkCore;


namespace AniDiaryBooks.Data;

public class AniDiaryBooksContext : DbContext
{
    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<CartItem> CartItems { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }

    public AniDiaryBooksContext(DbContextOptions<AniDiaryBooksContext> options) : base(options)
    {
    }
}