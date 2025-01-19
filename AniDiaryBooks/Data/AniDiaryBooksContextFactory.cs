using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;


namespace AniDiaryBooks.Data;

public class AniDiaryBooksContextFactory : IDesignTimeDbContextFactory<AniDiaryBooksContext>
{
    public AniDiaryBooksContext CreateDbContext(string[] args)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        var builder = new DbContextOptionsBuilder<AniDiaryBooksContext>();
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        builder.UseNpgsql(connectionString);

        return new AniDiaryBooksContext(builder.Options);
    }
}

