using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using AniDiaryBooks.Abstractions.Enums;
using AniDiaryBooks.Abstractions.Interfaces;
using AniDiaryBooks.Data;
using AniDiaryBooks.Helpers;
using AniDiaryBooks.Models;
using AniDiaryBooks.Views;
using Microsoft.Extensions.DependencyInjection;


namespace AniDiaryBooks.ViewModels;

public class BookListViewModel : INotifyPropertyChanged
{
    public ObservableCollection<Book> Books { get; set; } = new();
    public event PropertyChangedEventHandler PropertyChanged;
    public ICommand ShowBookDetailsCommand { get; }
    public ICommand EditBookCommand { get; }
    public ICommand AddBookCommand { get; }
    public ICommand DeleteBookCommand { get; }

    private readonly IBookRepository _bookRepository;
    private readonly IAuthorRepository _authorRepository;
    private readonly IGenreRepository _genreRepository;

    private User _currentUser;
    private Book _selectedBook;

    private string _windowTitle;
    private string _welcomeMessage;
    private Visibility _addBookButtonVisibility;
    private Visibility _editBookButtonVisibility;
    private Visibility _deleteBookButtonVisibility;
    private Visibility _ordersButtonVisibility;


    public BookListViewModel(IBookRepository bookRepository, IAuthorRepository authorRepository, IGenreRepository genreRepository)
    {
        _bookRepository = bookRepository;
        _authorRepository = authorRepository;
        _genreRepository = genreRepository;

        ShowBookDetailsCommand = new RelayCommand(ShowBookDetails);
        EditBookCommand = new RelayCommand(EditBook);
        AddBookCommand = new RelayCommand(AddBook);
        DeleteBookCommand = new RelayCommand(DeleteBook);
    }

    public Book SelectedBook
    {
        get => _selectedBook;
        set
        {
            _selectedBook = value;
            OnPropertyChanged();
        }
    }

    public string WindowTitle
    {
        get => _windowTitle;
        set
        {
            _windowTitle = value;
            OnPropertyChanged();
        }
    }

    public string WelcomeMessage
    {
        get => _welcomeMessage;
        set
        {
            _welcomeMessage = value;
            OnPropertyChanged();
        }
    }

    public Visibility AddBookButtonVisibility
    {
        get => _addBookButtonVisibility;
        set
        {
            _addBookButtonVisibility = value;
            OnPropertyChanged();
        }
    }

    public Visibility EditBookButtonVisibility
    {
        get => _editBookButtonVisibility;
        set
        {
            _editBookButtonVisibility = value;
            OnPropertyChanged();
        }
    }
    public Visibility DeleteBookButtonVisibility
    {
        get => _deleteBookButtonVisibility;
        set
        {
            _deleteBookButtonVisibility = value;
            OnPropertyChanged();
        }
    }
    public Visibility OrdersButtonVisibility
    {
        get => _ordersButtonVisibility;
        set
        {
            _ordersButtonVisibility = value;
            OnPropertyChanged();
        }
    }

    public async Task LoadDataAsync()
    {
        _currentUser = App.CurrentUser;
        SetUserRole();
        await LoadBooksAsync();
    }


    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));


    private async Task LoadBooksAsync()
    {
        var books = await _bookRepository.GetAllBooksAsync();
        Books = new ObservableCollection<Book>(books);

        OnPropertyChanged(nameof(Books));
    }

    private void EditBook()
    {
        if (SelectedBook == null)
            return;

        var bookAddEditWindow = new BookAddEditWindow(
            App.GetServiceProvider().GetRequiredService<AniDiaryBooksContext>(),
            App.GetServiceProvider().GetRequiredService<IAuthorRepository>(),
            App.GetServiceProvider().GetRequiredService<IGenreRepository>(),
            SelectedBook
        );

        if (bookAddEditWindow.ShowDialog() == true)
        {
            if (bookAddEditWindow.Book != null && bookAddEditWindow.Book.Id == 0)
                _bookRepository.AddBookAsync(bookAddEditWindow.Book);
            else if (bookAddEditWindow.Book != null)
                _bookRepository.UpdateBookAsync(bookAddEditWindow.Book);

            LoadBooksAsync();
        }
    }

    private void AddBook()
    {
        var bookAddEditWindow = new BookAddEditWindow(
            App.GetServiceProvider().GetRequiredService<AniDiaryBooksContext>(),
            App.GetServiceProvider().GetRequiredService<IAuthorRepository>(),
            App.GetServiceProvider().GetRequiredService<IGenreRepository>()
        );

        if (bookAddEditWindow.ShowDialog() == true)
        {
            if (bookAddEditWindow.Book != null)
            {
                _bookRepository.AddBookAsync(bookAddEditWindow.Book);
                LoadBooksAsync();
            }
        }
    }

    private async void DeleteBook()
    {
        if (SelectedBook == null)
        {
            MessageBox.Show("Выберите книгу для удаления.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }

        var result = MessageBox.Show(
            $"Вы действительно хотите удалить книгу '{SelectedBook.Title}'?",
            "Подтверждение удаления",
            MessageBoxButton.YesNo,
            MessageBoxImage.Question
        );

        if (result == MessageBoxResult.Yes)
        {
            await _bookRepository.DeleteBookAsync(SelectedBook);
            await LoadBooksAsync();
        }
    }

    private void ShowBookDetails()
    {
        if (SelectedBook == null)
            return;

        var bookDetailsWindow = new BookDetailsWindow();
        bookDetailsWindow.DataContext = new BookDetailsViewModel(SelectedBook);
        bookDetailsWindow.ShowDialog();
    }

    private void SetUserRole()
    {
        if (_currentUser != null)
        {
            WelcomeMessage = $"Добро пожаловать, {_currentUser.Username}!";
            switch (_currentUser.Role)
            {
                case UserRole.Admin:
                    WindowTitle = "Панель администратора";
                    AddBookButtonVisibility = Visibility.Visible;
                    EditBookButtonVisibility = Visibility.Visible;
                    DeleteBookButtonVisibility = Visibility.Visible;
                    OrdersButtonVisibility = Visibility.Visible;
                    break;
                case UserRole.User:
                    WindowTitle = "Книжный магазин";
                    AddBookButtonVisibility = Visibility.Collapsed;
                    EditBookButtonVisibility = Visibility.Collapsed;
                    DeleteBookButtonVisibility = Visibility.Collapsed;
                    OrdersButtonVisibility = Visibility.Collapsed;
                    break;
            }
        }
        else
        {
            WindowTitle = "Книжный магазин";
            WelcomeMessage = "Добро пожаловать!";
            AddBookButtonVisibility = Visibility.Collapsed;
            EditBookButtonVisibility = Visibility.Collapsed;
            DeleteBookButtonVisibility = Visibility.Collapsed;
            OrdersButtonVisibility = Visibility.Collapsed;
        }
    }
}