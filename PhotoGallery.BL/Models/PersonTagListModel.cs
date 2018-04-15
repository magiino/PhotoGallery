using System.Collections.Generic;
using PhotoGallery.DAL.Entities;

namespace PhotoGallery.BL.Models
{
    public class PersonTagListModel
    {
        public int Id { get; set; }
        public PersonEntity Person { get; set; }
        public int XPosition { get; set; }
        public int YPosition { get; set; }
    }
}