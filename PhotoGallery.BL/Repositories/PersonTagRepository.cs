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
        private readonly DataContext _dataContext;

        public PersonTagRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public PersonTagListModel GetById(int id)
        {
            var personTag = _dataContext.PersonTags.FirstOrDefault(x => x.Id == id);
            return Mapper.PersonTagEntityToPersonTagListModel(personTag);
        }
        public ICollection<PersonTagListModel> GetAll()
        {
            return Mapper.PersonTagEntitiesToPersonTagListModels(_dataContext.PersonTags.ToList());
        }
        public PersonTagListModel GetByFirstName(string name)
        {
            var personTag = _dataContext.PersonTags.FirstOrDefault(x => x.Person.FirstName == name);
            return Mapper.PersonTagEntityToPersonTagListModel(personTag);
        }
        public PersonTagListModel GetByLastName(string name)
        {
            var personTag = _dataContext.PersonTags.FirstOrDefault(x => x.Person.LastName == name);
            return Mapper.PersonTagEntityToPersonTagListModel(personTag);
        }

        public PersonTagEntity Add(PersonTagEntity person)
        {
            var addedPersonTag = _dataContext.PersonTags.Add(person);
            _dataContext.SaveChanges();
            return addedPersonTag;
        }

        public bool Delete(int id)
        {
            var person = _dataContext.PersonTags.FirstOrDefault(x => x.Id == id);
            if (person == null) return false;

            _dataContext.PersonTags.Remove(person);
            _dataContext.SaveChanges();
            return true;
        }

        public bool Update(PersonTagDetailModel person)
        {
            var personEntity = _dataContext.PersonTags.SingleOrDefault(x => x.Id == person.Id);
            if (personEntity == null) return false;

            personEntity.XPosition = person.XPosition;
            personEntity.YPosition = person.YPosition;
            personEntity.Person.FirstName = person.FirstName;
            personEntity.Person.LastName = person.LastName;
            _dataContext.SaveChanges();
            return true;
        }
    }
}