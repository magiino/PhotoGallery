using System.Collections.Generic;
using System.Linq;
using PhotoGallery.DAL;
using PhotoGallery.BL.Models;

namespace PhotoGallery.BL.Repositories
{
    public class PersonTagRepository
    {
        private readonly Mapper mapper = new Mapper();

        public ICollection<PersonTagModel> GetAll()
        {
            using (var dataContext = new DataContext())
            {
                return mapper.Map(dataContext.PersonTags.ToList());

            }
        }

        public PersonTagModel GetByFirstName(string name)
        {
            using (var dataContext = new DataContext())
            {
                var personTag = dataContext
                 .PersonTags
                 .FirstOrDefault(r => r.Person.FirstName == name);
                return mapper.Map(personTag);
            }
        }

        public PersonTagModel GetByLastName(string name)
        {
            using (var dataContext = new DataContext())
            {
                var personTag = dataContext
                 .PersonTags
                 .FirstOrDefault(r => r.Person.LastName == name);
                return mapper.Map(personTag);
            }
        }
    }
}