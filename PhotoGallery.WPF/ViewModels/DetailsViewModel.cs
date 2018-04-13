using System;
using PhotoGallery.BL;
using PhotoGallery.BL.Models;
using PhotoGallery.DAL.Enums;

namespace PhotoGallery.WPF.ViewModels
{
    public class DetailsViewModel
    {
        public string Name { get; set; }
        public DateTime CreatedTime { get; set; }
        public Format Format { get; set; }
        public ResolutionModel Resolution { get; set; }
        public string Note { get; set; }
        public string Location { get; set; }

        public DetailsViewModel(IMessenger messenger, IUnityOfWork albumRepository)
        {
            OnLoad();
        }

        private void OnLoad()
        {
            // fetch data
        }
    }
}