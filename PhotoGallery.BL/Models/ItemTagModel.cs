using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using PhotoGallery.DAL.Enums;
using PhotoGallery.DAL.Entities;

namespace PhotoGallery.BL.Models
{
    class ItemTagModel
    {
        public string Name { get; set; }

        public int PositionOnPhotoX { get; set; }
       
        public int PositionOnPhotoY { get; set; }

        public virtual ICollection<PhotoEntity> PhotosWithThisTag { get; set; } = new List<PhotoEntity>();

    }
}
