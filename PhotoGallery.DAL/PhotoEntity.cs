using System;
using System.Collections.Generic;

namespace PhotoGallery.DAL
{
    public class PhotoEntity
    {
        public int Id { get; set; }

        public string PhotoName { get; set; }

        public DateTime CreatedTime { get; set; }

        public Format Format { get; set; }

        public Resolution Resolution { get; set; }

        public string Note { get; set; }

        public string Location { get; set; }

        public ICollection<AlbumEntity> Albums { get; set; } = new List<AlbumEntity>();
    }
}
