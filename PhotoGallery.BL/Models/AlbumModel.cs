namespace PhotoGallery.BL.Models
{
    public class AlbumModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string CoverPhotoPath { get; set; }
        public int CoverPhotoId{ get; set; }
        public int NumberOfPhotos { get; set; }
    }
}