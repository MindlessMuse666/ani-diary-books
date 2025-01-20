using AniDiaryBooks.Models;


namespace AniDiaryBooks.Abstractions.Interfaces;

public interface IAuthorRepository
{
    Task<List<Author>> GetAllAuthorsAsync();
    Task<Author> GetAuthorByIdAsync(int id);
    Task AddAuthorAsync(Author author);
    Task UpdateAuthorAsync(Author author);
    Task DeleteAuthorAsync(Author author);
}