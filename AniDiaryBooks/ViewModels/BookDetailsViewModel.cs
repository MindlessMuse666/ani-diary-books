using AniDiaryBooks.Models;


namespace AniDiaryBooks.ViewModels;

public class BookDetailsViewModel
{
    public Book Book { get; }

    public BookDetailsViewModel(Book book)
    {
        Book = book;
    }
}