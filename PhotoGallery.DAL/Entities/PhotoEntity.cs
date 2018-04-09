using System;
using System.Collections.Generic;

using PhotoGallery.DAL.Entities.Base.Implementation;
using PhotoGallery.DAL.Enums;
using System.ComponentModel.DataAnnotations;

namespace PhotoGallery.DAL.Entities
{
    public class PhotoEntity : BaseEntity
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Path { get; set; }
        [Required]
        public DateTime CreatedTime { get; set; }
        [Required]
        public Format Format { get; set; }
        public int ResolutionId { get; set; }
        public ResolutionEntity Resolution { get; set; }
        public string Note { get; set; }
        public string Location { get; set; }
        [Required]
        public virtual ICollection<AlbumEntity> Albums { get; set; } = new List<AlbumEntity>();
        public virtual ICollection<TagEntity> Tags { get; set; } = new List<TagEntity>();
    }
}
