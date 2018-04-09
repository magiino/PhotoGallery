using PhotoGallery.DAL.Entities.Base.Implementation;

namespace PhotoGallery.DAL.Entities
{
    public class LocationEntity : BaseEntity
    {
        public double Longitude { get; set; }
        public double Latitude { get; set; }
    }
}