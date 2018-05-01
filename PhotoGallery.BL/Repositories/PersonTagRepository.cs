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

        public ICollection<PersonTagModel> GetByPersonId(int personId)
        {
            var itemTags = _dataContext.PersonTags.Where(x => x.PersonId == personId).ToList();
            return Mapper.PersonTagEntitiesToPersonTagModels(itemTags);
        }

        public int Add(TagModel person, PhotoDetailModel photo)
        {
            var name = person.Name.Split(' ');
            var fisrt = name[0];
            var last = name[1];
            var personEntity = _dataContext.Persons.FirstOrDefault(x => x.FirstName == fisrt);
            if (personEntity == null)
            {
                personEntity = new PersonEntity()
                {
                    FirstName = fisrt,
                    LastName = last
                };
                _dataContext.Persons.Add(personEntity);
            }
            _dataContext.SaveChanges();

            var photoEntity = _dataContext.Photos.SingleOrDefault(x => x.Id == photo.Id);

            if (photoEntity == null) return -1;
            var newPersonTag = new PersonTagEntity()
            {
                Person = personEntity,
                PersonId = personEntity.Id,
                XPosition = person.XPosition,
                YPosition = person.YPosition,
            };
            photoEntity.Tags.Add(newPersonTag);
            _dataContext.SaveChanges();

            return newPersonTag.Id;
        }

        public bool Delete(int id)
        {
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
    }
}