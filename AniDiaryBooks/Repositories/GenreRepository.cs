using AniDiaryBooks.Abstractions.Interfaces;
using AniDiaryBooks.Data;
using AniDiaryBooks.Models;
using Microsoft.EntityFrameworkCore;


namespace AniDiaryBooks.Repositories;

public class GenreRepository : IGenreRepository
{
    private readonly AniDiaryBooksContext _context;

    public GenreRepository(AniDiaryBooksContext context)
    {
        _context = context;
    }

    public async Task<List<Genre>> GetAllGenresAsync()
    {
        return await _context.Genres.ToListAsync();
    }

    public async Task<Genre> GetGenreByIdAsync(int id)
    {
        return await _context.Genres.FindAsync(id);
    }

    public async Task AddGenreAsync(Genre genre)
    {
        _context.Genres.Add(genre);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateGenreAsync(Genre genre)
    {
        _context.Genres.Update(genre);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteGenreAsync(Genre genre)
    {
        _context.Genres.Remove(genre);
        await _context.SaveChangesAsync();
    }
}