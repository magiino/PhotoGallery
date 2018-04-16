namespace PhotoGallery.BL.MessengerFile.Messeges
{
    public class SendChoosenPhoto
    {
        public int PhotoId { get; set; }

        public SendChoosenPhoto(int id)
        {
            PhotoId = id;
        }
    }
}