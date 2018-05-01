using PhotoGallery.BL.Models;

namespace PhotoGallery.BL.MessengerFile.Messeges
{
    public class SendAlbum
    {
        public AlbumModel AlbumModel { get; set; }
        public bool Delete { get; set; }

        public SendAlbum(AlbumModel albumModel,bool delete = false)
        {
            AlbumModel = albumModel;
            Delete = delete;
        }
    }
}