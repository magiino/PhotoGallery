using System.Collections.Generic;
using System.Linq;
using PhotoGallery.DAL;
using PhotoGallery.BL.Models;

namespace PhotoGallery.BL.Repositories
{
    public class PersonTagRepository
    {
        public ICollection<PersonTagListModel> GetAll()
        {
            using (var dataContext = new DataContext())
            {
                return Mapper.PersonTagEntitiesToPersonTagListModels(dataContext.PersonTags.ToList());

            }
        }

        public PersonTagListModel GetByFirstName(string name)
        {
            using (var dataContext = new DataContext())
            {
                var personTag = dataContext
                 .PersonTags
                 .FirstOrDefault(r => r.Person.FirstName == name);
                return Mapper.PersonTagEntityToPersonTagListModel(personTag);
            }
        }

        public PersonTagListModel GetByLastName(string name)
        {
            using (var dataContext = new DataContext())
            {
                var personTag = dataContext
                 .PersonTags
                 .FirstOrDefault(r => r.Person.LastName == name);
                return Mapper.PersonTagEntityToPersonTagListModel(personTag);
            }
        }
    }
}