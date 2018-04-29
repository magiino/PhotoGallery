namespace PhotoGallery.BL.Models
{
    public class ResolutionModel
    {
        public int Id { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public string Resolution => $"{Height} {Width}";

        public override string ToString()
        {
            return $"{Height} {Width}";
        }
    }
}