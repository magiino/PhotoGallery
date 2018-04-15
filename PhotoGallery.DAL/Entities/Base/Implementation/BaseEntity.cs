using System.ComponentModel.DataAnnotations;
using PhotoGallery.DAL.Entities.Base.Interface;

namespace PhotoGallery.DAL.Entities.Base.Implementation
{
    public abstract class BaseEntity : IEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
