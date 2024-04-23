using Avalonia.Controls;
using Avalonia.Interactivity;
using kursachRVV.Models;
using kursachRVV.Services;
using System;

namespace kursachRVV.Views
{
    public partial class AuthorizeWindow : Window
    {
        public AuthorizeWindow()
        {
            InitializeComponent();
        }
        private async void Authorize(object sender, RoutedEventArgs e)
        {
            try
            {
                string password = this.Find<TextBox>("Password")!.Text!;
                string login = this.Find<TextBox>("Login")!.Text!;
                Vhod user = await DBCall.Authorize(login, password);
                switch (user.Rol)
                {
                    case 1:
                        var adminWindow = new AdminWindow();
                        adminWindow.Show();
                        Close();
                        break;
                    case 2:
                        var TechDepartmentWindow = new TechDepartmentWindow(user);
                        TechDepartmentWindow.Show();
                        Close();
                        break;
                }
            }
            catch
            {
                Label ErrorLabel = this.Find<Label>("ErrorLabel");
                ErrorLabel.IsVisible = true;
                ErrorLabel.Content = "Ошибка соединения";
            }
        }
    }
}