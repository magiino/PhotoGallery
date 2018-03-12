using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoGallery.DAL
{
    abstract class Tag
    {


        //Tag(float xPos, float yPos)
        //{
        //    positionOnPhotox = xPos;
        //    positionOnPhotoy = yPos;
        //}

        public int PositionOnPhotoX { get; protected set; }
        public int PositionOnPhotoY { get; protected set; }



    }
}
