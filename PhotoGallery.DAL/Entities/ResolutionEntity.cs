using PhotoGallery.DAL.Entities.Base.Implementation;

namespace PhotoGallery.DAL.Entities
{
    public class ResolutionEntity : BaseEntity
    {
        public int Height { get; set; }
        public int Width { get; set; }
    }
}