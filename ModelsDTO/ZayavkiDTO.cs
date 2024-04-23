using kursachRVV.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace kursachRVV.ModelsDTO
{
    public class ZayavkiDTO : INotifyPropertyChanged
    {
        public int IdZayavki { get; set; }

        public string Opisanie { get; set; } = null!;

        private Srochnost _srochnost;

        public Srochnost Srochnost
        {
            get { return _srochnost; }
            set
            {
                _srochnost = value;
                OnPropertyChanged(nameof(Srochnost));
            }
        }


        public string Raspolozenie { get; set; } = null!;

        public DateOnly DateAndTime { get; set; }

        
        private Status _status;

        public Status Status
        {
            get { return _status; }
            set
            {
                _status = value;
                OnPropertyChanged(nameof(Status));
            }
        }

        private Ispolnitel _ispolnitel;

        public Ispolnitel Ispolnitel
        {
            get { return _ispolnitel; }
            set
            {
                _ispolnitel = value;
                OnPropertyChanged(nameof(Ispolnitel));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
