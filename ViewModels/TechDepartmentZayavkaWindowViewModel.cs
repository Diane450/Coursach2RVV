using kursachRVV.Models;
using kursachRVV.ModelsDTO;
using kursachRVV.Services;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kursachRVV.ViewModels
{
    public class TechDepartmentZayavkaWindowViewModel : ViewModelBase
    {
        public ZayavkiDTO SelectedZayavka { get; set; }

        private Status _selectedStatus;

        public Status SelectedStatus
        {
            get { return _selectedStatus; }
            set { _selectedStatus = this.RaiseAndSetIfChanged(ref _selectedStatus, value); }
        }

        private ObservableCollection<Status> _statuses;

        public ObservableCollection<Status> Statuses
        {
            get { return _statuses; }
            set { _statuses = this.RaiseAndSetIfChanged(ref _statuses, value); }
        }

        private string _message;

        public string Message
        {
            get { return _message; }
            set { _message = this.RaiseAndSetIfChanged(ref _message, value); }
        }

        private bool _isEnable;

        public bool IsEnable
        {
            get { return _isEnable; }
            set { _isEnable = this.RaiseAndSetIfChanged(ref _isEnable, value); }
        }
        private string _buttonText;

        public string ButtonText
        {
            get { return _buttonText; }
            set { _buttonText = this.RaiseAndSetIfChanged(ref _buttonText, value); }
        }


        public TechDepartmentWindowViewModel TechDepartmentWindowViewModel { get; set; }

        public Ispolnitel SelectedIspolnitel { get; private set; }

        public Vhod User { get; set; }

        public TechDepartmentZayavkaWindowViewModel(ZayavkiDTO zayavkiDTO, TechDepartmentWindowViewModel techDepartmentWindowViewModel, Vhod user)
        {
            SelectedZayavka = zayavkiDTO;
            TechDepartmentWindowViewModel = techDepartmentWindowViewModel;
            User = user;
            GetContent();
        }
        private async Task GetContent()
        {
            Statuses = await DBCall.GetAllStatuses();
            SelectedStatus = Statuses.First(s => s.IdStatys == SelectedZayavka.Status.IdStatys);
            if (SelectedZayavka.Ispolnitel == User.TexOtNavigation.IspolnitelIdIspolnitelNavigation)
            {
                IsEnable = true;
                ButtonText = "Сохранить изменения";
            }
            else if (SelectedZayavka.Ispolnitel != null)
            {
                IsEnable = false;
                Message = "Данная заявка уже в работе у другого исполнителя, ее нельзя изменить";
                ButtonText = "Сохранить изменения";
            }

            else
            {
                IsEnable = true;
                ButtonText = "Стать исполнителем";
            }
        }

        public async Task SaveChanges()
        {
            try
            {
                await DBCall.SaveZayavkaChanges(SelectedZayavka, SelectedStatus, User);
                SelectedZayavka.Status = SelectedStatus;
                SelectedZayavka.Ispolnitel = User.TexOtNavigation.IspolnitelIdIspolnitelNavigation;
                TechDepartmentWindowViewModel.IsIspolnitelDefined = true;
                TechDepartmentWindowViewModel.Filter();
                Message = "Изменения сохранены";
            }
            catch (Exception ex)
            {
                Message = "Не удалось сохранить изменения";
            }
        }
    }
}
