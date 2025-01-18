using AniDiaryBooks.ViewModels;
using System.Windows;


namespace AniDiaryBooks;

public partial class MainWindow : Window
{
    private readonly BookListViewModel _viewModel;

    public MainWindow(BookListViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        DataContext = _viewModel;
        Loaded += MainWindow_Loaded;
    }

    private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
    {
        await _viewModel.LoadBooksAsync();
    }
}