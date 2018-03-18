using System.ComponentModel.DataAnnotations;
using PhotoGallery.DAL.Entities.Base.Implementation;

namespace PhotoGallery.DAL.Entities
{
    public class ResolutionEntity : BaseEntity
    {
        [Required]
        public int Height { get; set; }
        [Required]
        public int Width { get; set; }
    }
}