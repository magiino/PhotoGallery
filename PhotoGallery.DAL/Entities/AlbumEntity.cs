using System.Collections.Generic;
using PhotoGallery.DAL.Entities.Base.Implementation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhotoGallery.DAL.Entities
{
    public class AlbumEntity : BaseEntity
    {
        [Required]
        public string Title { get; set; }
        public PhotoEntity CoverPhoto { get; set; }
        public ICollection<PhotoEntity> Photos {get ; set; } = new List<PhotoEntity>();
        [NotMapped]
        public int NumberOfPhotos => Photos.Count;
    }
}