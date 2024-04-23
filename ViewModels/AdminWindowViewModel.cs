using DynamicData;
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
    public class AdminWindowViewModel:ViewModelBase
    {
        public List<ZayavkiDTO> ZayavkiList { get; set; }

        private ObservableCollection<ZayavkiDTO> _zayavki;

        public ObservableCollection<ZayavkiDTO> Zayavki
        {
            get { return _zayavki; }
            set { _zayavki = this.RaiseAndSetIfChanged(ref _zayavki, value); }
        }

        private ObservableCollection<Status> _statuses;

        public ObservableCollection<Status> Statuses
        {
            get { return _statuses; }
            set { _statuses = this.RaiseAndSetIfChanged(ref _statuses, value); }
        }

        private ObservableCollection<Srochnost> _srochnosts;

        public ObservableCollection<Srochnost> Srochnosts
        {
            get { return _srochnosts; }
            set { _srochnosts = this.RaiseAndSetIfChanged(ref _srochnosts, value); }
        }

        private ZayavkiDTO _selectedZayavka;

        public ZayavkiDTO SelectedZayavka
        {
            get { return _selectedZayavka; }
            set { _selectedZayavka = this.RaiseAndSetIfChanged(ref _selectedZayavka, value); }
        }

        private Srochnost _selectedSrochnost;

        public Srochnost SelectedSrochnost
        {
            get { return _selectedSrochnost; }
            set { _selectedSrochnost = this.RaiseAndSetIfChanged(ref _selectedSrochnost, value); Filter(); }
        }

        private Status _selectedStatus;

        public Status SelectedStatus
        {
            get { return _selectedStatus; }
            set { _selectedStatus = this.RaiseAndSetIfChanged(ref _selectedStatus, value); Filter(); }
        }

        private string _message;

        public string Message
        {
            get { return _message; }
            set { _message = this.RaiseAndSetIfChanged(ref _message, value); }
        }

        private bool _isMessageVisible;

        public bool IsMessageVisible
        {
            get { return _isMessageVisible; }
            set { _isMessageVisible = this.RaiseAndSetIfChanged(ref _isMessageVisible, value); }
        }

        public AdminWindowViewModel()
        {
            GetContent();
        }
        private async Task GetContent()
        {
            try
            {
                ZayavkiList = await DBCall.GetAllZayavki();
                Srochnosts = await DBCall.GetAllSrochnosts();
                Statuses = await DBCall.GetAllStatuses();
                Zayavki = new ObservableCollection<ZayavkiDTO>(ZayavkiList);
                SelectedZayavka = Zayavki[0];
                SelectedSrochnost = Srochnosts[0];
                SelectedStatus = Statuses[0];
            }
            catch (Exception)
            {
                Message = "Ошибка соединения";
                IsMessageVisible = true;
            }
        }
        private void Filter()
        {
            var filteredList = new List<ZayavkiDTO>(ZayavkiList);

            if (SelectedStatus!=null)
            {
                filteredList = filteredList.Where(z => z.Status.IdStatys == SelectedStatus.IdStatys).ToList();
            }
            if (SelectedSrochnost!=null)
            {
                filteredList = filteredList.Where(z => z.Srochnost.Id == SelectedSrochnost.Id).ToList();
            }

            Zayavki.Clear();
            Zayavki.AddRange(filteredList);

            if (Zayavki.Count != 0)
            {
                Message = "";
                SelectedZayavka = Zayavki[0];
                IsMessageVisible = false;

            }
            else
            {
                Message = "Нет заявок по выбранным категориям";
                IsMessageVisible = true;
            }
        }
    }
}
