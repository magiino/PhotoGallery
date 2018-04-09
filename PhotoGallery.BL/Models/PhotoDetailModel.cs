using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using PhotoGallery.DAL.Enums;
using PhotoGallery.DAL.Entities;

namespace PhotoGallery.BL.Models
{
    class PhotoDetailModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public DateTime CreatedTime { get; set; }
        public Format Format { get; set; }
        public int ResolutionId { get; set; }
        public ResolutionEntity Resolution { get; set; }
        public string Note { get; set; }
        public int LocationId { get; set; }
        public LocationEntity Location { get; set; }
        public ICollection<AlbumEntity> Albums { get; set; }
        public ICollection<TagEntity> Tags { get; set; }
    }

}
