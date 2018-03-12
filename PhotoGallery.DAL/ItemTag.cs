using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoGallery.DAL
{
    class ItemTag : Tag
    {
        public string ItemName { get; private set; }
        ItemTag(string itemName, int xPos, int yPos)
        {
            ItemName = itemName;
            PositionOnPhotoX = xPos;
            PositionOnPhotoY = yPos;

        }


    }
}
