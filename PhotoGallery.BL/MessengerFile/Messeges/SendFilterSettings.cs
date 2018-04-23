using System;
using PhotoGallery.BL.Models;
using PhotoGallery.DAL.Enums;

namespace PhotoGallery.BL.MessengerFile.Messeges
{
    public class SendFilterSettings
    {
        public string SearchString { get; set; }
        public Sort Sort { get; set; }
        public bool SortAscending { get; set; }
        public Format Format { get; set; }
        public ResolutionModel ResolutionModel { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }

        public SendFilterSettings()
        {
        }
    }
}