using System;

namespace PhotoGallery.BL.Models
{
    public class PhotoListModel
    {
        public int Id { get; set; }
        public string Path { get; set; }
        public string Name { get; set; }
        public DateTime CreatedTime { get; set; }
    }
}