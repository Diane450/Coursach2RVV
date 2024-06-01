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
    public class AdminWindowViewModel : ViewModelBase
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
            set
            {
                _selectedZayavka = this.RaiseAndSetIfChanged(ref _selectedZayavka, value);
                IsIspolnitelDefined = SelectedZayavka?.Ispolnitel == null ? false : true;
            }
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

        private bool _isMessageVisible = true;

        public bool IsMessageVisible
        {
            get { return _isMessageVisible; }
            set { _isMessageVisible = this.RaiseAndSetIfChanged(ref _isMessageVisible, value); }
        }

        private bool _isIspolnitelDefined;

        public bool IsIspolnitelDefined
        {
            get { return _isIspolnitelDefined; }
            set { _isIspolnitelDefined = this.RaiseAndSetIfChanged(ref _isIspolnitelDefined, value); }
        }

        private ObservableCollection<string> _sortValues;

        public ObservableCollection<string> SortValues
        {
            get { return _sortValues; }
            set { _sortValues = this.RaiseAndSetIfChanged(ref _sortValues, value); }
        }

        private string _selectedSortValues;

        public string SelectedSortValues
        {
            get { return _selectedSortValues; }
            set { _selectedSortValues = this.RaiseAndSetIfChanged(ref _selectedSortValues, value); Sort(); }
        }

        private string _searchId;

        public string SearchId
        {
            get { return _searchId; }
            set
            {
                if(int.TryParse(value, out int id)|| value=="")
                    _searchId = this.RaiseAndSetIfChanged(ref _searchId, value);
            }
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

                SortValues = new ObservableCollection<string>()
                {
                    "Номер: По возрастанию",
                    "Номер: По убыванию",
                    "Дата: По возрастанию",
                    "Дата: По убыванию",
                };
                Zayavki = new ObservableCollection<ZayavkiDTO>(ZayavkiList);
                SelectedZayavka = Zayavki[0];
                SelectedSrochnost = Srochnosts[0];
                SelectedStatus = Statuses[0];
                SelectedSortValues = SortValues[0];
            }
            catch (Exception)
            {
                Message = "Ошибка соединения";
                IsMessageVisible = true;
            }
        }
        public void Filter()
        {
            var filteredList = new List<ZayavkiDTO>(ZayavkiList);

            if (SelectedStatus != null)
            {
                filteredList = filteredList.Where(z => z.Status.IdStatys == SelectedStatus.IdStatys).ToList();
            }
            if (SelectedSrochnost != null)
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
                Sort();
            }
            else
            {
                Message = "Нет заявок по выбранным категориям";
                IsMessageVisible = true;
            }
        }
        private void Sort()
        {
            if (SelectedSortValues == SortValues[0])
                Zayavki = new ObservableCollection<ZayavkiDTO>(Zayavki.OrderBy(x => x.IdZayavki).ToList());
            else if (SelectedSortValues == SortValues[1])
                Zayavki = new ObservableCollection<ZayavkiDTO>(Zayavki.OrderByDescending(x => x.IdZayavki).ToList());
            else if (SelectedSortValues == SortValues[2])
                Zayavki = new ObservableCollection<ZayavkiDTO>(Zayavki.OrderBy(x => x.DateAndTime).ToList());
            else
                Zayavki = new ObservableCollection<ZayavkiDTO>(Zayavki.OrderByDescending(x => x.DateAndTime).ToList());
        }

        public void Search()
        {
            if (SearchId != null && SearchId.Length != 0)
            {
                var zayavka = ZayavkiList.FirstOrDefault(z => z.IdZayavki == Convert.ToInt32(SearchId));
                if (zayavka == null)
                {
                    IsMessageVisible = true;
                    Message = $"Заявки по номеру {SearchId} не найдено";
                }
                else
                {
                    SelectedSrochnost = Srochnosts.First(s => s.Id == zayavka.Srochnost.Id);
                    SelectedStatus = Statuses.First(s => s.IdStatys == zayavka.Status.IdStatys);
                    SelectedZayavka = ZayavkiList.First(z => z.IdZayavki == zayavka.IdZayavki);
                }
            }
        }
    }
}
