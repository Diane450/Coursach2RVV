using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using kursachRVV.Models;
using kursachRVV.ModelsDTO;
using kursachRVV.ViewModels;

namespace kursachRVV;

public partial class TechDepartmentZayavkaWindow : Window
{
    public TechDepartmentZayavkaWindow(ZayavkiDTO zayavkiDTO, TechDepartmentWindowViewModel techDepartmentWindowViewModel, Vhod User)
    {
        InitializeComponent();
        DataContext = new TechDepartmentZayavkaWindowViewModel(zayavkiDTO, techDepartmentWindowViewModel, User);

    }
}