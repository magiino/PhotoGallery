namespace PhotoGallery.BL.MessengerFile.Messeges
{
    public class ChosenItem
    {
        public int Id { get; set; }
        public bool IsTag { get; set; }

        public ChosenItem(int id, bool isTag)
        {
            Id = id;
            IsTag = isTag;
        }
    }
}