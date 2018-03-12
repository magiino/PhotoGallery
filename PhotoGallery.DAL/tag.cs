using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoGallery.DAL
{
    abstract class Tag
    {


        Tag(float xPos, float yPos)
        {
            positionOnPhotox = xPos;
            positionOnPhotoy = yPos;
        }

        public float positionOnPhotox { get; set; }
        public float positionOnPhotoy { get; set; }

    }
}
