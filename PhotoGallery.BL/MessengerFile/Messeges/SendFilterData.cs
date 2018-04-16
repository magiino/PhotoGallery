namespace PhotoGallery.BL.MessengerFile.Messeges
{
    public class SendFilterData
    {
        public int PhotoId { get; set; }

        public SendFilterData(int id)
        {
            PhotoId = id;
        }
    }
}