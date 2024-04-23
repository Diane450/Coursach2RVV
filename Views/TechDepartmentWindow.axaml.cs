using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using kursachRVV.Models;
using kursachRVV.ViewModels;

namespace kursachRVV;

public partial class TechDepartmentWindow : Window
{
    public Vhod User { get; set; }
    public TechDepartmentWindow(Vhod user)
    {
        InitializeComponent();
        User = user;
        DataContext = new TechDepartmentWindowViewModel();
    }

    private async void ChangeZayavka(object sender, RoutedEventArgs e)
    {
        var button = (Button)sender;
        var dataContext = (TechDepartmentWindowViewModel)button.DataContext;
        var selectedZayavka = dataContext.SelectedZayavka;

        var window = new TechDepartmentZayavkaWindow(selectedZayavka, (TechDepartmentWindowViewModel)DataContext, User);
        window.ShowDialog(this);
    }
}