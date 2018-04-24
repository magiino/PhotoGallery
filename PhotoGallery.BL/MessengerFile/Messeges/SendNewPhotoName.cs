namespace PhotoGallery.BL.MessengerFile.Messeges
{
    public class SendNewPhotoName
    {
        public string PhotoName { get; set; }

        public SendNewPhotoName(string photoName)
        {
            PhotoName = photoName;
        }
    }
}