using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PhotoGallery.DAL.Entities.Base.Implementation;

namespace PhotoGallery.DAL.Entities
{
    public abstract class TagEntity : BaseEntity
   {
        [Required]
        public int PositionOnPhotoX { get; set; }
        [Required]
        public int PositionOnPhotoY { get; set; }
        public virtual ICollection<PhotoEntity> PhotosWithThisTag { get; set; } = new List<PhotoEntity>();
    }
}
