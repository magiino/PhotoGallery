using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoGallery.DAL.Entities
{
    public class PersonTag : Tag
    {
        

        public PersonEntity Person { get; private set; }

        PersonTag(string name, string surname, int xPos, int yPos)
        {
            
            PositionOnPhotoX = xPos;
            PositionOnPhotoY = yPos;
        }

    }
}
