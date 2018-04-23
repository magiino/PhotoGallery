using PhotoGallery.BL.Models;

namespace PhotoGallery.BL.MessengerFile.Messeges
{
    public class SendAddPhoto
    {
        public PhotoListModel PhotoModel { get; set; }
        public int AlbumId { get; set; }

        public SendAddPhoto(PhotoListModel photoModel, int albumId)
        {
            PhotoModel = photoModel;
            AlbumId = albumId;
        }
    }
}