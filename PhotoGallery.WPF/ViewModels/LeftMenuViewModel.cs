using PhotoGallery.BL.Models;
using System.Collections.ObjectModel;
using PhotoGallery.BL;
using PhotoGallery.BL.Repositories.Interfaces;
using PhotoGallery.DAL.Entities;

namespace PhotoGallery.WPF.ViewModels
{
    public class LeftMenuViewModel
    {
        public ObservableCollection<AlbumModel> Albums { get; set; }
        public AlbumModel SelectedAlbum { get; set; }

        public ObservableCollection<TagEntity> Tags { get; set; }
        public TagEntity SelectedTag { get; set; }

        public string AlbumSearch { get; set; }
        public string TagSearch { get; set; }

        public LeftMenuViewModel(IMessenger messenger, IUnitOfWork albumRepository)
        {
            OnLoad();
        }

        private void OnLoad()
        {
            // fetch data
        }
    }
}