using System.Collections.Generic;
using System.Linq;
using PhotoGallery.DAL;
using PhotoGallery.BL.Models;

namespace PhotoGallery.BL.Repositories
{
    public class PersonRepository //: IPersonRepository
    {
        private readonly DataContext _dataContext;

        public PersonRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public PersonModel GetById(int id)
        {
            var person = _dataContext.Persons.FirstOrDefault(x => x.Id == id);
            return Mapper.PersonEntityToPersonModel(person);
        }
        public ICollection<PersonModel> GetAll()
        {
            return Mapper.PersonEntitiesToPersonModels(_dataContext.Persons.ToList());
        }

        public bool Delete(int id)
        {
            var person = _dataContext.Persons.FirstOrDefault(x => x.Id == id);
            if (person == null) return false;

            _dataContext.Persons.Remove(person);
            _dataContext.SaveChanges();
            return true;
        }
    }
}