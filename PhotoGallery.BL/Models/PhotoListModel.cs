using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhotoGallery.DAL.Entities;

namespace PhotoGallery.BL.Models
{
    public class PhotoListModel
    {
        public string Path { get; set; }

        public string Name { get; set; }

        public ICollection<AlbumEntity> Albums;
    }
}
