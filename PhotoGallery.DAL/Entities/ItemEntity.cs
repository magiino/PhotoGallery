using PhotoGallery.DAL.Entities.Base.Implementation;
using System.ComponentModel.DataAnnotations;

namespace PhotoGallery.DAL.Entities
{
    public class ItemEntity : BaseEntity
    { 
        [Required]
        public string Name { get; set; }
    }
}