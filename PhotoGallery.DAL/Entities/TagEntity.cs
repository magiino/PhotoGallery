using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PhotoGallery.DAL.Entities.Base.Implementation;

namespace PhotoGallery.DAL.Entities
{
    public abstract class TagEntity : BaseEntity
   {
        [Required]
        public int XPosition { get; set; }
        [Required]
        public int YPosition { get; set; }
        public virtual ICollection<PhotoEntity> Photos { get; set; } = new List<PhotoEntity>();
    }
}