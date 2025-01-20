using AniDiaryBooks.Abstractions.Interfaces;
using AniDiaryBooks.Data;
using AniDiaryBooks.Models;
using AniDiaryBooks.Repositories;
using AniDiaryBooks.ViewModels;
using AniDiaryBooks.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using System.Windows;


namespace AniDiaryBooks;

public partial class App : Application
{
    public static User? CurrentUser { get; set; }
    public static ServiceProvider GetServiceProvider() => (App.Current as App)._serviceProvider;

    private IConfigurationRoot _configuration;
    private ServiceProvider _serviceProvider;


    public App()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json");
        _configuration = builder.Build();

        _serviceProvider = (ServiceProvider)ConfigureServices();

        using (var scope = _serviceProvider.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<AniDiaryBooksContext>();
            DataInitializer.Initialize(context);
        }
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        var loginWindow = _serviceProvider.GetRequiredService<LoginWindow>();
        loginWindow.Show();
    }

    private IServiceProvider ConfigureServices()
    {
        var services = new ServiceCollection();
        services.AddDbContext<AniDiaryBooksContext>(options =>
            options.UseNpgsql(_configuration.GetConnectionString("DefaultConnection")));

        // Регистрация View и Services:
        services.AddScoped<MainWindow>();
        services.AddScoped<Services.Auth.AuthService>();
        services.AddScoped<LoginWindow>();

        // Регистрация репозиториев:
        services.AddScoped<IBookRepository, BookRepository>();
        services.AddScoped<IAuthorRepository, AuthorRepository>();
        services.AddScoped<IGenreRepository, GenreRepository>();
        services.AddScoped<IUserRepository, UserRepository>();

        // Регистрация ViewModels:
        services.AddScoped<BookListViewModel>();

        return services.BuildServiceProvider();
    }
}