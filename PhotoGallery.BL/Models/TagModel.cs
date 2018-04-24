namespace PhotoGallery.BL.Models
{
    public class TagModel
    {
        public bool IsItem { get; set; }
        public int Id { get; set; }
        public int XPosition { get; set; }
        public int YPosition { get; set; }
        public int PersonId { get; set; }
        public int ItemId { get; set; }
        public string Name { get; set; }
    }
}