using System;
using System.ComponentModel.DataAnnotations.Schema;
using PhotoGallery.DAL.Entities.Base.Implementation;

namespace PhotoGallery.DAL.Entities
{
    public class ResolutionEntity : BaseEntity
    {
        public string Resolution
        {
            get => $"{Height} {Width}";
            set
            {
                if (value == null) return;
                var split = value.Split(' ');
                Height = Convert.ToInt32(split[0]);
                Width = Convert.ToInt32(split[1]);
            }
        }
        [NotMapped]
        public int Height { get; set; }
        [NotMapped]
        public int Width { get; set; }
    }
}