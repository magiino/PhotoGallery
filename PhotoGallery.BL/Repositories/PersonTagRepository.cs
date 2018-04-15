using System.Collections.Generic;
using System.Linq;
using PhotoGallery.DAL;
using PhotoGallery.BL.Models;
using PhotoGallery.DAL.Entities;

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


        private readonly DataContext dataDontext;

        public PersonTagRepository(DataContext dataDontext)
        {
            dataDontext = dataDontext;
        }

        public PersonTagModel GetById(int id)
        {
            using (var dataContext = new DataContext())
            {
                var personTag = dataContext
                    .PersonTags
                    .FirstOrDefault(r => r.Id == id);
                return mapper.Map(personTag);
            }
        }
        public PersonTagEntity Add(PersonTagEntity person)
        {
            dataDontext.PersonTags.Add(person);
            dataDontext.SaveChanges();
            return person;
        }


    }
}