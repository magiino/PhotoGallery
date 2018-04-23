namespace PhotoGallery.BL.MessengerFile.Messeges
{
    public class SendDeletePhoto
    {
        public int PhotoId { get; set; }
        public int AlbumId { get; set; }

        public SendDeletePhoto(int photoId, int albumId)
        {
            PhotoId = photoId;
            AlbumId = albumId;
        }
    }
}