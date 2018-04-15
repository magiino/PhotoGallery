using System.ComponentModel.DataAnnotations;

namespace PhotoGallery.DAL.Entities
{
    public class ItemTagEntity : TagEntity
    {
        [Required]
        public string Name { get; set; }
    }
}