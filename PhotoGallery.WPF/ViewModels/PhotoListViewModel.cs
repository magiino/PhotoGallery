using PhotoGallery.BL.Models;
using System.Collections.ObjectModel;
using PhotoGallery.BL;

namespace PhotoGallery.WPF.ViewModels
{
    public class PhotoListViewModel
    {
        public ObservableCollection<PhotoListModel> Photos { get; set; }
        public PhotoListModel SelectedPhoto { get; set; }

        public PhotoListViewModel(IMessenger messenger, IUnitOfWork albumRepository)
        {
            OnLoad();
        }

        private void OnLoad()
        {
            // fetch data
        }
    }
}