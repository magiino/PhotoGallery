using PhotoGallery.BL.Models;
using System.Collections.ObjectModel;

namespace PhotoGallery.WPF.ViewModels
{
    public class LeftMenuViewModel
    {
        public ObservableCollection<AlbumModel> Albums { get; set; }
        public string Search { get; set; }



        public LeftMenuViewModel()
        {
            OnLoad();
        }


        private void OnLoad()
        {
            // fetch data
        }
    }
}
