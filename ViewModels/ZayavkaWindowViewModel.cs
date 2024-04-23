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
    public class ZayavkaWindowViewModel : ViewModelBase
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

        private ObservableCollection<Ispolnitel> _ispolnitels;

        public ObservableCollection<Ispolnitel> Ispolnitels
        {
            get { return _ispolnitels; }
            set { _ispolnitels = this.RaiseAndSetIfChanged(ref _ispolnitels, value); }
        }

        private Ispolnitel _selectedIspolnitel;

        public Ispolnitel SelectedIspolnitel
        {
            get { return _selectedIspolnitel; }
            set { _selectedIspolnitel = this.RaiseAndSetIfChanged(ref _selectedIspolnitel, value); }
        }

        private string _message;

        public string Message
        {
            get { return _message; }
            set { _message = this.RaiseAndSetIfChanged(ref _message, value); }
        }

        public AdminWindowViewModel AdminWindowViewModel { get; set; }

        public ZayavkaWindowViewModel(ZayavkiDTO zayavkiDTO, AdminWindowViewModel adminWindowViewModel)
        {
            SelectedZayavka = zayavkiDTO;
            AdminWindowViewModel = adminWindowViewModel;
            GetContent();
        }
        private async Task GetContent()
        {
            Statuses = await DBCall.GetAllStatuses();
            Ispolnitels = await DBCall.GetAllIspolnitels();
            SelectedStatus = Statuses.First(s => s.IdStatys == SelectedZayavka.Status.IdStatys);
            SelectedIspolnitel = Ispolnitels[0];
        }

        public async Task SaveChanges()
        {
            try
            {
                await DBCall.SaveZayavkaChanges(SelectedZayavka, SelectedIspolnitel, SelectedStatus);
                SelectedZayavka.Status = SelectedStatus;
                SelectedZayavka.Ispolnitel = SelectedIspolnitel;
                AdminWindowViewModel.IsIspolnitelDefined = true;
                AdminWindowViewModel.Filter();
                Message = "Изменения сохранены";
            }
            catch (Exception ex)
            {
                Message = "Не удалось сохранить изменения";
            }
        }
    }
}
