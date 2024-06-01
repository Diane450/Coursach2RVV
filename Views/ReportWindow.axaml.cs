using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using kursachRVV.ViewModels;

namespace kursachRVV;

public partial class ReportWindow : Window
{
    public ReportWindow()
    {
        InitializeComponent();
        DataContext = new ReportWindowViewModel();
    }
}