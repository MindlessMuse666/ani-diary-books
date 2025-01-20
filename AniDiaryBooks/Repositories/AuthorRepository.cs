using AniDiaryBooks.Abstractions.Interfaces;
using AniDiaryBooks.Data;
using AniDiaryBooks.Models;
using Microsoft.EntityFrameworkCore;


namespace AniDiaryBooks.Repositories;

public class AuthorRepository : IAuthorRepository
{
    private readonly AniDiaryBooksContext _context;

    public AuthorRepository(AniDiaryBooksContext context)
    {
        _context = context;
    }

    public async Task<List<Author>> GetAllAuthorsAsync()
    {
        return await _context.Authors.ToListAsync();
    }

    public async Task<Author> GetAuthorByIdAsync(int id)
    {
        return await _context.Authors.FindAsync(id);
    }

    public async Task AddAuthorAsync(Author author)
    {
        _context.Authors.Add(author);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAuthorAsync(Author author)
    {
        _context.Authors.Update(author);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAuthorAsync(Author author)
    {
        _context.Authors.Remove(author);
        await _context.SaveChangesAsync();
    }
}