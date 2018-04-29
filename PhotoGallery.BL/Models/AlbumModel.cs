using System.ComponentModel;
using PropertyChanged;

namespace PhotoGallery.BL.Models
{
    [AddINotifyPropertyChangedInterface]
    public class AlbumModel : INotifyPropertyChanged
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string CoverPhotoPath { get; set; }
        public int CoverPhotoId{ get; set; }
        public int NumberOfPhotos { get; set; }

        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };
    }
}