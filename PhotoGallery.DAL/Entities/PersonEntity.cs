using PhotoGallery.DAL.Entities.Base.Implementation;

namespace PhotoGallery.DAL.Entities
{
    public class PersonEntity : BaseEntity
    { 
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
    }
}
