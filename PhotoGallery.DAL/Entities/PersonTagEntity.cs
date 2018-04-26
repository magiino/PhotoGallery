namespace PhotoGallery.DAL.Entities
{
    public class PersonTagEntity : TagEntity
    {
        public int PersonId { get; set; }
        public PersonEntity Person { get; set; }
    }
}