using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using kursachRVV.Models;
using kursachRVV.ViewModels;
using kursachRVV.Views;

namespace kursachRVV;

public partial class AdminWindow : Window
{
    public AdminWindow()
    {
        InitializeComponent();
        DataContext = new AdminWindowViewModel();
    }
    private async void ChangeZayavka(object sender, RoutedEventArgs e)
    {
        var button = (Button)sender;
        var dataContext = (AdminWindowViewModel)button.DataContext;
        var selectedZayavka = dataContext.SelectedZayavka;

        var window = new ZayavkaWindow(selectedZayavka, (AdminWindowViewModel)DataContext);
        window.ShowDialog(this);
    }
    private void OpenReportWindow(object sender, RoutedEventArgs e)
    {
        var window = new ReportWindow();
        window.ShowDialog(this);
    }
}