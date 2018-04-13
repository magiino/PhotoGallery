using System.Windows.Input;
using PhotoGallery.BL;
using PhotoGallery.BL.Models;

namespace PhotoGallery.WPF.ViewModels
{
    public class PhotoDetailViewModel
    {
        public PhotoListModel Photo { get; set; }
        public int CurrentAlbumId { get; set; }

        public ICommand PreviousPhoto { get; set; }
        public ICommand NextPhoto { get; set; }

        public PhotoDetailViewModel(IMessenger messenger, IUnitOfWork albumRepository)
        {
            OnLoad();
            PreviousPhoto = new RelayCommand(GetPreviousPhoto);
            NextPhoto = new RelayCommand(GetNextPhoto);
        }

        private void GetPreviousPhoto()
        {
            // fetch 
        }

        private void GetNextPhoto()
        {
            // fetch
        }

        private void OnLoad()
        {
            // fetch data
        }
    }
}