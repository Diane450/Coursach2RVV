using ReactiveUI;
using System;

namespace kursachRVV.ViewModels
{
    public class AuthorizeWindowViewModel : ViewModelBase
    {
        private string _password;

        public string Password
        {
            get { return _password; }
            set { _password = this.RaiseAndSetIfChanged(ref _password, value); }
        }

        private string _login;

        public string Login
        {
            get { return _login; }
            set { _login = this.RaiseAndSetIfChanged(ref _login, value); }
        }

        private bool _isAuthEnable;

        public bool IsAuthEnable
        {
            get { return _isAuthEnable; }
            set { _isAuthEnable = this.RaiseAndSetIfChanged(ref _isAuthEnable, value); }
        }

        public AuthorizeWindowViewModel()
        {
            this.WhenAnyValue(x => x.Login, x => x.Password).Subscribe(_ => AuthEnable());
        }
        private void AuthEnable()
        {
            IsAuthEnable = !string.IsNullOrEmpty(Login) && !string.IsNullOrEmpty(Password);
        }
    }
}