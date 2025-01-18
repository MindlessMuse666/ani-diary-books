namespace AniDiaryBooks.Data;

public class MainWindowFactory
{
    private readonly AniDiaryBooksContext _context;

    public MainWindowFactory(AniDiaryBooksContext context)
    {
        _context = context;
    }
}