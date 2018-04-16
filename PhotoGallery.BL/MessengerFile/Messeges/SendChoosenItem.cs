namespace PhotoGallery.BL.MessengerFile.Messeges
{
    public class SendChoosenItem
    {
        public int Id { get; set; }
        public bool IsTag { get; set; }

        public SendChoosenItem(int id, bool isTag)
        {
            Id = id;
            IsTag = isTag;
        }
    }
}