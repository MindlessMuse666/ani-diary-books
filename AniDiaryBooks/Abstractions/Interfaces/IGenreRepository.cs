using AniDiaryBooks.Models;


namespace AniDiaryBooks.Abstractions.Interfaces;

public interface IGenreRepository
{
    Task<List<Genre>> GetAllGenresAsync();
    Task<Genre> GetGenreByIdAsync(int id);
    Task AddGenreAsync(Genre genre);
    Task UpdateGenreAsync(Genre genre);
    Task DeleteGenreAsync(Genre genre);
}