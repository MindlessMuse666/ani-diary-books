using AniDiaryBooks.ViewModels;
using System.Windows;


namespace AniDiaryBooks.Views;

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
        await _viewModel.LoadDataAsync();
    }

    private void BooksDataGrid_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        _viewModel.ShowBookDetailsCommand.Execute(null);
    }
}