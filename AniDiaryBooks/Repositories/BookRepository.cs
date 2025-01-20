using AniDiaryBooks.Abstractions.Interfaces;
using AniDiaryBooks.Data;
using AniDiaryBooks.Models;
using Microsoft.EntityFrameworkCore;


namespace AniDiaryBooks.Repositories;

public class BookRepository : IBookRepository
{
    private readonly AniDiaryBooksContext _context;

    public BookRepository(AniDiaryBooksContext context)
    {
        _context = context;
    }

    public async Task<List<Book>> GetAllBooksAsync()
    {
        return await _context.Books.Include(b => b.Author).Include(b => b.Genre).ToListAsync();
    }

    public async Task<Book> GetBookByIdAsync(int id)
    {
        return await _context.Books.FindAsync(id);
    }

    public async Task AddBookAsync(Book book)
    {
        _context.Books.Add(book);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateBookAsync(Book book)
    {
        _context.Books.Update(book);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteBookAsync(Book book)
    {
        _context.Books.Remove(book);
        await _context.SaveChangesAsync();
    }
}