using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PhotoGallery.DAL.Enums;
using PhotoGallery.DAL.Entities;
namespace PhotoGallery.BL.Models
{
    public class PersonTagModel
    {
        public int PersonId { get; set; }

        public PersonEntity Person { get; set; }

        public int PositionOnPhotoX { get; set; }

        public int PositionOnPhotoY { get; set; }

        public ICollection<PhotoEntity> PhotosWithThisTag { get; set; } = new List<PhotoEntity>();

    }
}
