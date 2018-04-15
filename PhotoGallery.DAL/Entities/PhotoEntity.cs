using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PhotoGallery.DAL.Entities.Base.Implementation;
using PhotoGallery.DAL.Enums;

namespace PhotoGallery.DAL.Entities
{
    public class PhotoEntity : BaseEntity
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Path { get; set; }
        public DateTime CreatedTime { get; set; }
        [Required]
        public Format Format { get; set; }
        [ForeignKey("Resolution")]
        public int ResolutionId { get; set; }
        public ResolutionEntity Resolution { get; set; }
        public string Note { get; set; }
        public string Location { get; set; }
        [ForeignKey("Album")]
        public int AlbumId { get; set; }
        public AlbumEntity Album { get; set; }
        public virtual ICollection<TagEntity> Tags { get; set; } = new List<TagEntity>();
    }
}
