using System;
using System.Collections.Generic;

namespace PhotoGallery.DAL
{
    public class AlbumEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Title { get; set; }
        public PhotoEntity CoverPhoto { get; set; }
        public ICollection<PhotoEntity> Photos {get ; set; } = new List<PhotoEntity>();
    }
}