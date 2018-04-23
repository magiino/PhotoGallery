namespace PhotoGallery.BL.MessengerFile.Messeges
{
    public class SendChosenPhoto
    {
        public int PhotoId { get; set; }

        public SendChosenPhoto(int id)
        {
            PhotoId = id;
        }
    }
}