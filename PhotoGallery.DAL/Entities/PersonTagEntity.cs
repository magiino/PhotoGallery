using System.ComponentModel.DataAnnotations.Schema;

namespace PhotoGallery.DAL.Entities
{
    public class PersonTagEntity : TagEntity
    {
        [ForeignKey("Person")]
        public int PersonId { get; set; }
        public PersonEntity Person { get; set; }
    }
}