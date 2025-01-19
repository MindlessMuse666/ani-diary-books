using AniDiaryBooks.Data;
using AniDiaryBooks.Models;
using Microsoft.EntityFrameworkCore;
using System.Windows;


namespace AniDiaryBooks.Views;

public partial class BookAddEditWindow : Window
{
    public Book? Book { get; private set; }
    private readonly AniDiaryBooksContext _context;
    private List<Author> _authors;
    private List<Genre> _genres;

    public BookAddEditWindow(AniDiaryBooksContext context, Book? book = null)
    {
        InitializeComponent();
        _context = context;
        Book = book;
        LoadData();
        DataContext = this;
    }

    private async void LoadData()
    {
        _authors = await _context.Authors.ToListAsync();
        _genres = await _context.Genres.ToListAsync();

        AuthorComboBox.ItemsSource = _authors;
        GenreComboBox.ItemsSource = _genres;

        if (Book != null)
        {
            TitleTextBox.Text = Book.Title;
            AuthorComboBox.SelectedItem = _authors.FirstOrDefault(a => a.Id == Book.AuthorId);
            GenreComboBox.SelectedItem = _genres.FirstOrDefault(g => g.Id == Book.GenreId);
            PriceTextBox.Text = Book.Price.ToString();
            DescriptionTextBox.Text = Book.Description;
            CoverImageUrlTextBox.Text = Book.CoverImageUrl;
            QuantityTextBox.Text = Book.Quantity.ToString();

        }
    }

    private void SaveButton_Click(object sender, RoutedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(TitleTextBox.Text) ||
           AuthorComboBox.SelectedItem == null ||
           GenreComboBox.SelectedItem == null ||
           string.IsNullOrWhiteSpace(PriceTextBox.Text) ||
           string.IsNullOrWhiteSpace(QuantityTextBox.Text)
          )
        {
            MessageBox.Show("Пожалуйста, заполните все обязательные поля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }

        if (!decimal.TryParse(PriceTextBox.Text, out decimal price) || price <= 0)
        {
            MessageBox.Show("Пожалуйста, введите корректную цену.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }

        if (!int.TryParse(QuantityTextBox.Text, out int quantity) || quantity <= 0)
        {
            MessageBox.Show("Пожалуйста, введите корректное количество.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }


        if (Book == null)
        {
            Book = new Book();
        }
        Book.Title = TitleTextBox.Text;
        Book.AuthorId = ((Author)AuthorComboBox.SelectedItem).Id;
        Book.GenreId = ((Genre)GenreComboBox.SelectedItem).Id;
        Book.Price = price;
        Book.Description = DescriptionTextBox.Text;
        Book.CoverImageUrl = CoverImageUrlTextBox.Text;
        Book.Quantity = quantity;

        DialogResult = true;
        Close();
    }
}