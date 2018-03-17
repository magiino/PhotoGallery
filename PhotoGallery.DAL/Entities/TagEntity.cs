using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PhotoGallery.DAL.Entities
{
   public abstract class TagEntity
   {
        [Required]
        public int PositionOnPhotoX { get; set; }
        [Required]
        public int PositionOnPhotoY { get; set; }
        public virtual ICollection<PhotoEntity> PhotosWithThisTag { get; set; } = new List<PhotoEntity>();
   }
}
