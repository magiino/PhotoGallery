using PhotoGallery.DAL.Enums;

namespace PhotoGallery.BL.MessengerFile.Messeges
{
    public class ChosenItem
    {
        public int Id { get; set; }
        public ItemType ItemType { get; set; }

        public ChosenItem(int id, ItemType itemType)
        {
            Id = id;
            ItemType = itemType;
        }
    }
}