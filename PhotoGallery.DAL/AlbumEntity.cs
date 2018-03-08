using System;
using System.Collections.Generic;

namespace PhotoGallery.DAL
{
    public class AlbumEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public PhotoEntity CoverPhoto { get; set; }
        public ICollection<PhotoEntity> Photos {get ; set; } = new List<PhotoEntity>();
    }
}