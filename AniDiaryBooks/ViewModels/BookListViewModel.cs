using AniDiaryBooks.Data;
using AniDiaryBooks.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Threading.Tasks;


namespace AniDiaryBooks.ViewModels;

public class BookListViewModel
{
    private readonly AniDiaryBooksContext _context;

    public ObservableCollection<Book> Books { get; set; } = new();

    public BookListViewModel(AniDiaryBooksContext context)
    {
        _context = context;
    }

    public async Task LoadBooksAsync()
    {
        var books = await _context.Books.Include(b => b.Author).Include(b => b.Genre).ToListAsync();
        foreach (var book in books)
        {
            Books.Add(book);
        }
    }
}