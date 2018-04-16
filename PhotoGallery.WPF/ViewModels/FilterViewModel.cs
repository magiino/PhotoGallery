using System;
using PhotoGallery.WPF.ViewModels.Base;
using PhotoGallery.BL.Models;
using System.Collections.Generic;
using System.Linq;
using PhotoGallery.DAL.Enums;

namespace PhotoGallery.WPF.ViewModels
{
    public class FilterViewModel : BaseViewModel 
    {

        //Tohle si nejsem uplně jisty jak se da pouzit podle ceho se to vlastne bude filtrovat

        //public bool FilterByName { get; set; } = true;
        //public bool FilterByResolution { get; set; } = true;
        //public bool FilterByDate { get; set } = true;
        //public bool FilterByFormat { get; set; } = true;

        public bool SortByName { get; set; } = false;
        public bool SortByDate { get; set; } = false;
        public bool SortByFormat { get; set; } = true;

        public bool SortAscending { get; set; } = true;
        public bool SortDescending { get; set; } = true;

        public string FilterByName { get; set; }

        public IEnumerable<Format> EFormat { get; set; } = Enum.GetValues(typeof(Format)).Cast<Format>();
        public Format SelectedFormat { get; set; }

        public IList<ResolutionModel> Resolution { get; set; }
        public ResolutionModel SelectedResolution { get; set; }

        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
    }
}