using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PhotoGallery.DAL.Entities
{
   public abstract class Tag
    {


        public int PositionOnPhotoX { get; protected set; }
        public int PositionOnPhotoY { get; protected set; }

        public ICollection<PhotoEntity> PhotosWithThisTag{get; set;} =  new List<PhotoEntity>();


    }
}
