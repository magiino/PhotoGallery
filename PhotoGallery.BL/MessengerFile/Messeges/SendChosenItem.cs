namespace PhotoGallery.BL.MessengerFile.Messeges
{
    public class SendChosenItem
    {
        public int Id { get; set; }
        public bool IsTag { get; set; }

        public SendChosenItem(int id, bool isTag)
        {
            Id = id;
            IsTag = isTag;
        }
    }
}