using System;
using System.Collections.Generic;
using PhotoGallery.DAL.Entities.Base.Implementation;

namespace PhotoGallery.DAL.Entities
{
    public class PhotoEntity : BaseEntity
    {
        public string PhotoName { get; set; }
        public DateTime CreatedTime { get; set; }
        public Format Format { get; set; }
        public Resolution Resolution { get; set; }
        public string Note { get; set; }
        public string Location { get; set; }
        public ICollection<AlbumEntity> Albums { get; set; } = new List<AlbumEntity>();
    }
}
