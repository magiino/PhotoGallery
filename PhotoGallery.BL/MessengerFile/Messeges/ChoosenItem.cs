namespace PhotoGallery.BL.MessengerFile.Messeges
{
    public class ChoosenItem
    {
        public int Id { get; set; }
        public bool IsTag { get; set; }

        public ChoosenItem(int id, bool isTag)
        {
            Id = id;
            IsTag = isTag;
        }
    }
}