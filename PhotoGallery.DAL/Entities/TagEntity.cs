using System.Collections.Generic;

namespace PhotoGallery.DAL.Entities
{
   public abstract class TagEntity
   {
        public int PositionOnPhotoX { get; set; }
        public int PositionOnPhotoY { get; set; }
        public virtual ICollection<PhotoEntity> PhotosWithThisTag { get; set; } = new List<PhotoEntity>();
   }
}
