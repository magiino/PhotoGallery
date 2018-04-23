using System.Collections.Generic;
using PhotoGallery.BL.Models;
using PhotoGallery.DAL.Entities;

namespace PhotoGallery.BL.Repositories.Interfaces
{
    public interface IPersonTagRepository
    {
        PersonTagModel GetById(int id);
        ICollection<PersonTagModel> GetAll();
        PersonTagModel GetByFirstName(string name);
        PersonTagModel GetByLastName(string name);

        PersonTagEntity Add(PersonTagEntity person);
        bool Delete(int id);
        bool Update(PersonTagModel person);
    }
}