using PhotoGallery.DAL.Entities.Base.Implementation;
using System.ComponentModel.DataAnnotations;

namespace PhotoGallery.DAL.Entities
{
    public class PersonEntity : BaseEntity
    { 
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
    }
}
