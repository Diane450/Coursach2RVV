using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using kursachRVV.ModelsDTO;
using kursachRVV.ViewModels;

namespace kursachRVV;

public partial class ZayavkaWindow : Window
{
    public ZayavkaWindow(ZayavkiDTO zayavkiDTO)
    {
        InitializeComponent();
        DataContext = new ZayavkaWindowViewModel(zayavkiDTO);
    }
}