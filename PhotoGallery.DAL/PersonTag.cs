using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoGallery.DAL
{
    class PersonTag : Tag
    {
        public string PersonName { get; private set; }
        public string PersonSurname { get; private set; }

        PersonTag(string name, string surname, int xPos, int yPos)
        {
            PersonName = name;
            PersonSurname = surname;
            PositionOnPhotoX = xPos;
            PositionOnPhotoY = yPos;
        }

    }
}
