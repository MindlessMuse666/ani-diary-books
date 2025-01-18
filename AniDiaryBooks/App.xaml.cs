using AniDiaryBooks.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using System.Windows;


namespace AniDiaryBooks;

public partial class App : Application
{
    private ServiceProvider _serviceProvider;
    private IConfigurationRoot _configuration;

    public App()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json");
        _configuration = builder.Build();

        _serviceProvider = (ServiceProvider)ConfigureServices();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
        mainWindow.Show();
    }

    private IServiceProvider ConfigureServices()
    {
        var services = new ServiceCollection();
        services.AddDbContext<AniDiaryBooksContext>(options =>
            options.UseNpgsql(_configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<MainWindow>();
        services.AddScoped<ViewModels.BookListViewModel>();

        return services.BuildServiceProvider();
    }
}