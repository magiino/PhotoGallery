using System.Collections.Generic;
using PhotoGallery.BL.Models;
using PhotoGallery.DAL.Entities;

namespace PhotoGallery.BL.Repositories.Interfaces
{
    public interface IPersonTagRepository
    {
        ICollection<PersonTagModel> GetAll();
        PersonTagModel GetByFirstName(string name);
        PersonTagModel GetByLastName(string name);


        PersonTagModel GetById(int id);
        PersonTagEntity Add(PersonTagEntity person);
        void Delete(int id);
        void Update(PersonTagModel person);
    }
}