using PhotoGallery.DAL.Entities.Base.Interface;

namespace PhotoGallery.DAL.Entities.Base.Implementation
{
    public abstract class BaseEntity : IEntity
    {
        public int Id { get; set; }
    }
}
