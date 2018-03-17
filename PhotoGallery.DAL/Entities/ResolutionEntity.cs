using PhotoGallery.DAL.Entities.Base.Implementation;
using System.ComponentModel.DataAnnotations;

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