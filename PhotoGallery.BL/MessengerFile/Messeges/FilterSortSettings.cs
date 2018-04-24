using System;
using PhotoGallery.DAL.Enums;

namespace PhotoGallery.BL.MessengerFile.Messeges
{
    public class FilterSortSettings
    {
        public string SearchString { get; set; }
        public Sort Sort { get; set; }
        public bool SortAscending { get; set; }
        public Format Format { get; set; }
        public int ResolutionId { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
    }
}