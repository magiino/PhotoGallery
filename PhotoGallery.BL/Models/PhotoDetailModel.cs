using System;
using System.Collections.Generic;
using PhotoGallery.DAL.Enums;
using PhotoGallery.DAL.Entities;

namespace PhotoGallery.BL.Models
{
    public class PhotoDetailModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public DateTime CreatedTime { get; set; }
        public Format Format { get; set; }
        public ResolutionEntity Resolution { get; set; }
        public string Note { get; set; }
        public LocationEntity Location { get; set; }
        public ICollection<TagEntity> Tags { get; set; }
    }

}
