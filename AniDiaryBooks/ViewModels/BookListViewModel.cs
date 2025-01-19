using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using AniDiaryBooks.Data;
using AniDiaryBooks.Models;
using Microsoft.EntityFrameworkCore;
using AniDiaryBooks.Abstractions.Enums;
using AniDiaryBooks.Views;
using AniDiaryBooks.Helpers;


namespace AniDiaryBooks.ViewModels;

public class BookListViewModel : INotifyPropertyChanged
{
    public ObservableCollection<Book> Books { get; set; } = new();
    public ICommand ShowBookDetailsCommand { get; }
    public ICommand EditBookCommand { get; }

    private readonly AniDiaryBooksContext _context;
    private User _currentUser;
    private Book _selectedBook;

    private string _windowTitle;
    private string _welcomeMessage;
    private Visibility _addBookButtonVisibility;
    private Visibility _editBookButtonVisibility;
    private Visibility _deleteBookButtonVisibility;
    private Visibility _ordersButtonVisibility;


    public BookListViewModel(AniDiaryBooksContext context)
    {
        _context = context;

        ShowBookDetailsCommand = new RelayCommand(ShowBookDetails);
        EditBookCommand = new RelayCommand(EditBook);
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
    private async Task LoadBooksAsync()
    {
        var books = await _context.Books.Include(b => b.Author).Include(b => b.Genre).ToListAsync();
        Books.Clear();
        foreach (var book in books)
        {
            Books.Add(book);
        }
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

    private void EditBook()
    {
        if (SelectedBook == null) return;

        var bookAddEditWindow = new BookAddEditWindow(_context, SelectedBook);
        if (bookAddEditWindow.ShowDialog() == true)
        {
            _context.SaveChanges();
            LoadBooksAsync();
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }



    private void ShowBookDetails()
    {
        if (SelectedBook == null) return;

        var bookDetailsWindow = new BookDetailsWindow();
        bookDetailsWindow.DataContext = new BookDetailsViewModel(SelectedBook);
        bookDetailsWindow.ShowDialog();
    }


}