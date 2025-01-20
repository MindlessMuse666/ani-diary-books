using AniDiaryBooks.Services.Auth;
using AniDiaryBooks.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;


namespace AniDiaryBooks.Views;

public partial class LoginWindow : Window
{
    private readonly AuthService _authService;

    public LoginWindow(AuthService authService)
    {
        InitializeComponent();
        _authService = authService;
    }

    private async void LoginButton_Click(object sender, RoutedEventArgs e)
    {
        string username = UsernameTextBox.Text;
        string password = PasswordTextBox.Text;

        var authResult = await _authService.AuthenticateAsync(username, password);

        if (authResult.IsAuthenticated)
        {
            // Сохраняем пользователя в статическое поле, чтобы его можно было использовать по всему приложению
            App.CurrentUser = authResult.AuthenticatedUser;
            var mainWindow = new MainWindow(
                App.GetServiceProvider().GetRequiredService<BookListViewModel>()
            );

            mainWindow.Show();
            Close();
        }
        else
            MessageBox.Show(authResult.ErrorMessage, "Ошибка авторизации", MessageBoxButton.OK, MessageBoxImage.Error);
    }
}