namespace PhotoGallery.DAL.Entities
{
    public class PersonTagEntity : TagEntity
    {
        public int PersonId { get; set; }
        public PersonEntity Person { get; set; }

        PersonTagEntity(string name, string surname, int xPos, int yPos)
        {
            Person.FirstName = name;
            Person.LastName = surname;
            PositionOnPhotoX = xPos;
            PositionOnPhotoY = yPos;
        }
    }
}
