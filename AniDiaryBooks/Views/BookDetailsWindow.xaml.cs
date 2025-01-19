using System.Windows;


namespace AniDiaryBooks.Views;

public partial class BookDetailsWindow : Window
{
    public BookDetailsWindow()
    {
        InitializeComponent();
    }

    private void Close_Click(object sender, RoutedEventArgs e)
    {
        Close();
    }
}