using System.Collections.Generic;
using PhotoGallery.DAL.Entities.Base.Implementation;

namespace PhotoGallery.DAL.Entities
{
    public class AlbumEntity : BaseEntity
    {
        public string Title { get; set; }
        public PhotoEntity CoverPhoto { get; set; }
        public ICollection<PhotoEntity> Photos {get ; set; } = new List<PhotoEntity>();
    }
}