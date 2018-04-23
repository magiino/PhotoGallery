using PhotoGallery.BL.Models;

namespace PhotoGallery.BL.MessengerFile.Messeges
{
    public class SendAlbum
    {
        public AlbumModel AlbumModel { get; set; }

        public SendAlbum(AlbumModel albumModel)
        {
            AlbumModel = albumModel;
        }
    }
}