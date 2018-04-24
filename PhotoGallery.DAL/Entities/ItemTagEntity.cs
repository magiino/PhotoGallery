namespace PhotoGallery.DAL.Entities
{
    public class ItemTagEntity : TagEntity
    {
        public int ItemId { get; set; }
        public ItemEntity Item { get; set; }
    }
}