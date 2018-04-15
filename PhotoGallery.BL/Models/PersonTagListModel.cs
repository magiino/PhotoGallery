using PhotoGallery.DAL.Entities;

namespace PhotoGallery.BL.Models
{
    public class PersonTagListModel
    {
        public int Id { get; set; }
        public PersonEntity Person { get; set; }
    }
}