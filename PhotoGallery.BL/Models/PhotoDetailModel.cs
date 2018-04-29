using System;
using System.Collections.Generic;
using PhotoGallery.DAL.Enums;

namespace PhotoGallery.BL.Models
{
    public class PhotoDetailModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public DateTime CreatedTime { get; set; }
        public Format Format { get; set; }
        public ResolutionModel Resolution { get; set; }
        public string Note { get; set; }
        public string Location { get; set; }
        public int AlbumId { get; set; }
        public List<TagModel> Tags { get; set; }
    }
}