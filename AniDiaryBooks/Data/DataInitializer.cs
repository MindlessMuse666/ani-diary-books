using AniDiaryBooks.Models;


namespace AniDiaryBooks.Data;

public static class DataInitializer
{
    public static void Initialize(AniDiaryBooksContext context)
    {
        context.Database.EnsureCreated();
        if (context.Books.Any())
        {
            return;
        }

        var authors = new List<Author>
            {
                new() { Name = "Джон Р.Р. Толкин" },
                new() { Name = "Агата Кристи" },
                new() { Name = "Рэй Брэдбери" },
                new() { Name = "Фёдор Достоевский"}
             };
        context.Authors.AddRange(authors);
        context.SaveChanges();

        var genres = new List<Genre>
            {   
                new() { Name = "Фэнтези" },
                new() { Name = "Детектив" },
                new() { Name = "Научная фантастика" },
                new() { Name = "Классика"}
             };
        context.Genres.AddRange(genres);
        context.SaveChanges();

        var books = new List<Book>
            {
                new() { Title = "Властелин колец: Братство кольца", AuthorId = authors[0].Id, GenreId = genres[0].Id, Price = 500, Description = "Первая книга трилогии", Quantity = 10 },
                new() { Title = "Десять негритят", AuthorId = authors[1].Id, GenreId = genres[1].Id, Price = 350, Description = "Детектив", Quantity = 15 },
                new() { Title = "451 градус по Фаренгейту", AuthorId = authors[2].Id, GenreId = genres[2].Id, Price = 400, Description = "Антиутопия", Quantity = 12 },
                new() { Title = "Преступление и наказание", AuthorId = authors[3].Id, GenreId = genres[3].Id, Price = 600, Description = "Классика", Quantity = 7}
            };
        context.Books.AddRange(books);
        context.SaveChanges();
    }
}
