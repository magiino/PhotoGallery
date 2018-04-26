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

        public PersonTagModel GetById(int id)
        {
            var personTag = _dataContext.PersonTags.FirstOrDefault(x => x.Id == id);
            return Mapper.PersonTagEntityToPersonTagModel(personTag);
        }
        public ICollection<PersonTagModel> GetAll()
        {
            return Mapper.PersonTagEntitiesToPersonTagModels(_dataContext.PersonTags.ToList());
        }
        public PersonTagModel GetByFirstName(string name)
        {
            var personTag = _dataContext.PersonTags.FirstOrDefault(x => x.Person.FirstName == name);
            return Mapper.PersonTagEntityToPersonTagModel(personTag);
        }
        public PersonTagModel GetByLastName(string name)
        {
            var personTag = _dataContext.PersonTags.FirstOrDefault(x => x.Person.LastName == name);
            return Mapper.PersonTagEntityToPersonTagModel(personTag);
        }


        public int Add(TagModel person, PhotoDetailModel photoDetailModel)
        {
            var name = person.Name.Split(' ');
            var personEntity = _dataContext.Persons.FirstOrDefault(x => x.FirstName == name[0]) ?? _dataContext.Persons.Add(new PersonEntity()
            {
                FirstName = name[0],
                LastName = name[1]
            });
            _dataContext.SaveChanges();

            var addedPersonTag = _dataContext.PersonTags.Add(new PersonTagEntity()
            {
                Person = personEntity,
                PersonId = personEntity.Id,
                XPosition = person.XPosition,
                YPosition = person.YPosition,
            });
            _dataContext.SaveChanges();
            return addedPersonTag.Id;
        }

        public bool Delete(int id)
        {
            // TODO v DB nastavit cascade delete z fotky
            var person = _dataContext.PersonTags.FirstOrDefault(x => x.Id == id);
            if (person == null) return false;

            var deletePersonEntity = true;
            foreach (var personTag in _dataContext.PersonTags)
            {
                if (personTag.PersonId != person.PersonId) continue;
                deletePersonEntity = false;
                break;
            }

            if (deletePersonEntity)
                _dataContext.Persons.Remove(person.Person);
            
            _dataContext.PersonTags.Remove(person);
            _dataContext.SaveChanges();
            return true;
        }

        public bool Update(PersonTagModel person)
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