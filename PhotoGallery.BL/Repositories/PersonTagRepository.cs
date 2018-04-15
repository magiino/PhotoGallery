using System.Collections.Generic;
using System.Linq;
using PhotoGallery.DAL;
using PhotoGallery.BL.Models;
using PhotoGallery.DAL.Entities;

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


        private readonly DataContext dataDontext;

        public PersonTagRepository(DataContext dataDontext)
        {
            dataDontext = dataDontext;
        }

        public PersonTagModel GetById(int id)
        {
                var personTag = dataDontext
                    .PersonTags
                    .FirstOrDefault(r => r.Id == id);
                return mapper.Map(personTag);
        }
        public PersonTagEntity Add(PersonTagEntity person)
        {
            dataDontext.PersonTags.Add(person);
            dataDontext.SaveChanges();
            return person;
        }

        public void Delete(int id)
        {
            var person = dataDontext.PersonTags.FirstOrDefault(r => r.Id == id);
            dataDontext.PersonTags.Remove(person);
        }

        public void Update(PersonTagModel person)
        {

        }

    }
}