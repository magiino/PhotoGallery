namespace PhotoGallery.DAL.Entities
{
    public class ItemTagEntity : TagEntity
    {
        public string Name { get; set; }

        ItemTagEntity(string name, int xPos, int yPos)
        {
            Name = name;
            PositionOnPhotoX = xPos;
            PositionOnPhotoY = yPos;
        }
    }
}
