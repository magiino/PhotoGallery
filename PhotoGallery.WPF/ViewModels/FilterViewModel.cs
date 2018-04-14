using System;
using PhotoGallery.WPF.ViewModels.Base;
using System.Collections.ObjectModel;
using PhotoGallery.BL.Models;
using PhotoGallery.BL;
using System.Collections.Generic;
using PhotoGallery.DAL.Enums;

namespace PhotoGallery.WPF.ViewModels
{
    public class FilterViewModel : BaseViewModel 
    // Netuším, proč to nechce správně podědit (řádek 11), ale Visual Studio po mě neřve, že nemám implementovanou metodu NotifyPropertyChanged();
    // U toho IEnumerable (řádek 39) si myslím, že to nevezme ty formáty co má.... podle mě chybí reference na DAL vrstvu... ale not sure... už v tom mám trochu bordel.
    {

        //Tohle si nejsem uplně jisty jak se da pouzit podle ceho se to vlastne bude filtrovat

        //public bool FilterByName { get; set; } = true;
        //public bool FilterByResolution { get; set; } = true;
        //public bool FilterByDate { get; set } = true;
        //public bool FilterByFormat { get; set; } = true;


        public bool SortByName { get; set; } = true;
        public bool SortByDate { get; set; } = true;
        public bool SortByFormat { get; set; } = true;

        public bool SortAscending { get; set; } = true;
        public bool SortDescending { get; set; } = true;


        private string _inputName;
        public string InputName
        {
            get => _inputName;
            set
            {
                _inputName = value;
                PhotoFilter(null);
            }
        }

        public IEnumerable<Format> EFormat { get; set; } = Enum.GetValues(typeof(Format)).Cast<Format>();
        private Format _selectedFormat;
        public Format SelectedFormat
        {
            get => _selectedFormat;
            set
            {
                _selectedFormat = value;
                PhotoFilter(null);
            }
        }

        public IList<ResolutionEntity> Resolution { get; set; } = Enum.GetValues(typeof(ResolutionEntity)).Cast<ResolutionEntity>();
        private Resolution _selectedResolution;
        public Resolution SelectedResolution
        {
            get => _selectedResolution;
            set
            {
                _selectedResolution = value;
                PhotoFilter(null);
            }
        }

        //public IList<ResolutionEntity> Resolution { get; set; } = Predicate <PhotoListModel> Photos; //Timhle si fakt nejsem jisty....
        //private static bool FindResol(ResolutionEntity obj)
        //{
        //    return 
        //}

        private DateTime? _dateFrom;
        private DateTime? _dateTo;

        public DateTime? DateFrom
        {
            get => _dateFrom;
            set
            {
                _dateFrom = value;
                NotifyPropertyChanged();
            }
        }

        public DateTime? DateTo
        {
            get => _dateTo;
            set
            {
                _dateTo = value;
                NotifyPropertyChanged();
            }
        }

        private void PhotoFilter(object parameter) //PhotoUtils není vytvořené, nemám ponětí jak ho napsat
        {
            var filterName = PhotoUtils.SelectByName(_discoveredPhoto, InputName);
            var filterDateFrom = PhotoUtils.SelectByDateFrom(filterName, DateFrom());
            var filterDateTo = PhotoUtils.SelectByDateTo(filterDateFrom, DateTo());
            var filterResolution = PhotoUtils.SelectByResolution(filterDateTo, SelectedResolution());

            FilteredPhotos = new ObservableCollection<PhotoListModel>(filterResolution.Select(x => new PhotoListModel(x)));
        }



        /*
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        */
    }
}
