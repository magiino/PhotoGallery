using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoGallery.BL.Repositories
{
    using PhotoGallery.DAL;
    using PhotoGallery.BL.Models;
    using PhotoGallery.BL;
    class PersonTagRepository
    {
        private readonly Mapper mapper = new Mapper();

        public ICollection<PersonTagModel> GetAll()
        {
            using (var dataContext = new DataContext())
            {
                return this.mapper.Map(dataContext.PersonTags.ToList());

            }

        }

        public PersonTagModel GetByFirstName(string name)
        {
            using (var dataContext = new DataContext())
            {
                var personTag = dataContext
                 .PersonTags
                 .FirstOrDefault(r => r.Person.FirstName == name);
                return this.mapper.Map(personTag);
            }

        }

        public PersonTagModel GetByLastName(string name)
        {
            using (var dataContext = new DataContext())
            {
                var personTag = dataContext
                 .PersonTags
                 .FirstOrDefault(r => r.Person.LastName == name);
                return this.mapper.Map(personTag);
            }

        }

    }
}
