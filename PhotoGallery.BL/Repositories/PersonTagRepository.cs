using System.Collections.Generic;
using System.Linq;
using PhotoGallery.DAL;
using PhotoGallery.BL.Models;
using PhotoGallery.BL.Repositories.Interfaces;
using PhotoGallery.DAL.Entities;

namespace PhotoGallery.BL.Repositories
{
    public class PersonTagRepository : IPersonTagRepository
    {
        private readonly DataContext _dataDontext;

        public PersonTagRepository(DataContext dataDontext)
        {
            _dataDontext = dataDontext;
        }

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

        public PersonTagListModel GetById(int id)
        {
            using (var dataContext = new DataContext())
            {
                var personTag = dataContext
                    .PersonTags
                    .FirstOrDefault(r => r.Id == id);
                return Mapper.PersonTagEntityToPersonTagListModel(personTag);
            }
        }
        public PersonTagEntity Add(PersonTagEntity person)
        {
            _dataDontext.PersonTags.Add(person);
            _dataDontext.SaveChanges();
            return person;
        }

        public void Delete(int id)
        {
            var person = _dataDontext.PersonTags.FirstOrDefault(r => r.Id == id);
            _dataDontext.PersonTags.Remove(person);
        }

        public void Update(PersonTagListModel person)
        {
        }


    }
}