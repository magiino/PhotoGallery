using System;
using System.Collections.Generic;
using PhotoGallery.DAL.Entities.Base.Implementation;
using PhotoGallery.DAL.Enums;

namespace PhotoGallery.DAL.Entities
{
    public class PhotoEntity : BaseEntity
    {
        public string Name { get; set; }
        public DateTime CreatedTime { get; set; }
        public Format Format { get; set; }
        public ResolutionEntity Resolution { get; set; }
        public string Note { get; set; }
        public string Location { get; set; }
        public virtual ICollection<AlbumEntity> Albums { get; set; } = new List<AlbumEntity>();
        public virtual ICollection<TagEntity> Tags { get; set; } = new List<TagEntity>();
    }
}
